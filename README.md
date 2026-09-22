# Tank StateMachine Prototype

> 一个 **2D 物理平台 / 坦克题材的玩法原型**，Unity 独立开发。
>
> 它的核心不是玩法内容，而是**一套「配置 / 数据驱动」的游戏框架**——把角色的「状态机、动作、输入、技能」全部抽象成可配置、可组合的单元，让「加一个新行为」尽量只做配置、不改代码。实践中落地了**三层架构（System / Model / View）分离、ScriptableObject + JSON 驱动的状态机、零 GC 的输入事件管线、对象池**等设计。

---

## 项目状态

> ⚠️ **这是一个框架原型，不是可玩的游戏。** 请先读这一节再读代码。

- **内容少、未完成**：`EnemyMachine`、`RunState`、`IdleState` 仍是空壳 MonoBehaviour（有的甚至没继承 `BaseState`）；`InputJointAction`、`CannonDeployer.Launch` 等方法体为空；`EnemyMachine` 没有任何逻辑。它是「框架原型」而非「完整游戏」。
- **框架有欠账**：
  - **魔法字符串泛滥** —— `Context` 用 `"moveDirection"`、`"jumpHeight"` 这类字符串键，拼错只能在运行时才发现，没有编译期校验（这是 Context 数据袋方案换来的代价）。
  - **死代码 / 注释掉的旧逻辑** —— `BroadcasterManager.InitRegisters` 还引用旧实现，很多方法留空。
  - **字段可见性随意** —— `public` 字段偏多、命名不统一。
  - 部分注释因编码问题出现乱码。
- **缺工程化**：无单元测试、无构建配置、无性能剖析。

它证明的是**框架抽象能力和性能意识**，不是「游戏好不好玩」。定位为「一个用来验证『配置驱动 + 三层架构 + 零 GC 输入』想法的原型」。

---

## 技术栈

| 项 | 内容 |
|---|---|
| 引擎 | Unity **2022.3.57f1c1**（Unity 2022.3 LTS，中国版），2D，物理用 `Rigidbody2D` |
| 渲染 | URP 14.0.11 |
| 语言 | C#，约 130 个脚本（不含插件） |
| 输入 | Unity Input System 1.11.2 |
| 第三方 | TextMesh Pro 3.0.7、Timeline 1.7.6 |
| 玩法 | 2D 侧视角物理过关：坦克（走 / 跑 / 翻滚 / 跳 / 坠落）+ 可挂载炮塔（Jointer）+ 抛射物（加农炮 / 炸弹） |

**一句话说明玩法**：操控一辆坦克在 2D 场景里移动、翻滚、跳跃，通过「挂点（Jointer）」装载不同武器（Deployer），发射抛射物（Projectile）攻击敌人、越过障碍。

---

## 核心设计：三层架构分离

这是理解整个项目的地图：

| 层 | 职责 | 规模 |
|---|---|---|
| **`System/`** | 可复用的「引擎层」——状态机框架、输入管线、注册表、对象池等通用机制，**不包含任何具体玩法** | 50 个文件 |
| **`Model/`** | 具体「内容层」——具体状态（Jump / Walk / Roll / Fire…）、具体输入动作、具体武器、具体条件判断 | 35 个文件 |
| **`View/`** | UI、音频等表现层 | 7 个文件 |
| **`Tools/`** | 纯数据结构与工具（对象池、环形缓冲） | 10 个文件 |
| **`Res/`** | Prefab、ScriptableObject、JSON 配置 | — |

> 把「框架」和「内容」彻底分开，是这个项目最核心的意图——框架可以在下一个项目复用，内容只负责填空。

---

## 架构设计

### 1. 数据驱动的状态机框架（本项目的灵魂）

角色的行为（走、跳、滚、开火）不该是一堆散落的 `if/else`，而应该用「配置」描述成一张**状态图**，运行时由通用引擎解析执行。

**四层抽象 + 一个工厂**：

1. **状态** `BaseState`（ScriptableObject）——最小的行为单元。每个状态只需实现一个帧逻辑（通过标记接口 `IFixedUpdate2<StateInformation, Context>`）：
   - `JumpState` → `AddForce(up * jumpHeight)`
   - `WalkState` → `AddForce(walkSpeed * moveDirection)`
   - `RollState` → `AddTorque(...)`
   - `FireState` → `JointsManager.CurrentLaunch()`
2. **转换** `BaseTransition` + **前提** `BasePremise` —— 切换条件。转换判断「是否该切」，前提是附加的守卫（guard）：`StandingPremise` 用 `Physics2D.Raycast` 判断是否着地，`InputPremise` 判断输入动作编号是否匹配。
3. **状态图** `StateClip` —— 运行时的图，`Dictionary<string, StateNode>` 存所有节点，每个节点挂自己的转换列表。`Switch` 每帧遍历转换，命中就切到 `toKey`。
4. **配置** `StateClipConfig` —— 状态的**声明式描述**（状态 + 转换 + 前提 + 参数），既可以在 Inspector 里用 ScriptableObject 配，也可以**序列化成 JSON**（`jsonConfig` + `JsonUtility`）运行时反序列化。
5. **工厂** `StateClipFactory` —— 把配置「编译」成运行时的 `StateClip`。内置三个特殊状态：`Any` / `OnEnter` / `OnExit`。

> **配置驱动 + 工厂模式**：状态图是数据，不是代码。加一个「下蹲」「冲刺」状态 = 新增一个 `BaseState` 子类 + 配置里加一个节点，主流程零改动。
> **`[SerializeReference]` 多态**承载「状态 / 转换 / 前提」的子类实例，是这套框架成立的关键。
> **JSON 热更思路**：状态机用 JSON 描述，理论上可以不改代码、只改 JSON 就调整角色行为（项目里还没做热更，但结构预留了）。

**代码**：`Assets/Scripts/System/StateSystem/BaseStateMachine.cs`、`BaseState.cs`、`StateClip.cs`、`Assets/Scripts/System/Factories/StateClipFactory.cs`、`Assets/Scripts/Model/StateModel/States/*.cs`

### 2. Context 数据袋 — 让状态与具体角色解耦

状态（State）不知道自己在驱动「坦克」还是「炮塔」，只从统一的数据袋里读参数，这样状态可复用、机器可扩展。

- 每个状态机（`MarioMachine`、`JointsMachine`）在 `InitContext` 里往 `Context` 塞自己的参数：`moveDirection`、`walkSpeed`、`jumpHeight`、`fallSpeed`、`rollSpeed`、`jointsManager`、`jumpEvent`、`fireEvent`…
- `Context` 是四个 `Dictionary<string, float/object/Action>` 的集合（Update / FixedUpdate 各一套）。
- 状态只读字符串键：`m.fixedUpdateFloatDetails["jumpHeight"]`——**它不关心这是谁、值从哪来**。

> 「依赖反转」的一种朴素实现——状态依赖「数据约定」而非「具体类」；代价是字符串键有拼写风险（见项目状态一节的诚实交代）。

**代码**：`Assets/Scripts/System/StateSystem/BaseStateMachine.cs`（`Context` 结构体）、`Assets/Scripts/Model/StateModel/StateMachines/MarioMachine.cs`

### 3. 输入管线 — Broadcaster → Action → Condition 三段解耦

输入事件不直接调角色方法，而是像消息一样「广播 → 排队 → 分发」，把「什么时候触发」和「谁处理」分开。

1. **Broadcaster（广播器）**：`BaseBroadcaster`（如 `InputBC`）订阅 Input System 的 `performed/started/canceled` 事件，把事件打包成 `ActionStack`（action + 输入上下文）丢给 `ActionManager`。
2. **Action（动作）**：`BaseAction` 子类（如 `InputStateAction`）决定这条输入该在 Update 还是 FixedUpdate 处理，并把输入转换成「条件」发到对应状态机（`CondSenderRegisters` → `InputCondSender`，开火键则发给 `JointsMachine`）。
3. **Condition（条件）**：`BaseConditionSender` 构造一个 `Condition`（含 `InputCondition` / `FallingCondition`），调 `stateMachine.Switch(ref condition)`，驱动状态机做转换判断。

> 典型的「事件 → 命令队列 → 分发」管线，各环节职责单一、可替换；输入处理与状态机逻辑完全解耦。

**代码**：`Assets/Scripts/System/InputSystem/`（`ActionManager`、`BaseBroadcaster`、`ControlsManager`）、`Assets/Scripts/Model/InputModel/Broadcasters/InputBC.cs`、`Assets/Scripts/Model/InputModel/Actions/InputStateAction.cs`

### 4. 零 GC 的环形缓冲 + 双缓冲输入队列

输入事件高频产生，每帧 `new List` 会带来 GC 压力；同时要避免「物理帧里处理输入」和「逻辑帧读输入」打架。

- 手写 `CircularArray<T>`（环形缓冲 + `CircularPointer` 游标，重载 `+` / `==` / `<` 等运算符），预分配固定大小。
- `ActionManager` 用**双缓冲**：`UpdateDatas()` 里把「未使用」和「使用中」两个环**交换引用**（`(a, b) = (b, a)`），输入事件写入一块、上一帧读另一块，读完 `Clear`。

> ① 预分配 + 环形复用 → **零 GC 分配**；② 双缓冲 → 读写在两个缓冲区上互不干扰；③ 手写数据结构体现对底层机制的理解（环形指针的取模运算、留一个空位区分「满 / 空」）。

**代码**：`Assets/Scripts/Tools/CircularArray.cs`、`Assets/Scripts/System/InputSystem/ActionManager/ActionManager.cs`

### 5. 注册表模式 — 手动组合根 + ScriptableObject 依赖注入

状态、转换、前提、广播器等「模块」由谁创建、由谁持有？——用一个集中的「注册表」按名字注册和查找，避免散落的单例和硬引用。

- 每种模块配一个 `XXXRegisters`（`StateRegisters` / `TransitionRegisters` / `PremiseRegisters` / `CondSenderRegisters` / `BroadCasterRegisters`…），持有 `[SerializeReference] BaseXXX[]` 数组，`Register()` 时填进 `Dictionary<string, T>`。
- `GameManager.Awake` 是一个**手动组合根**：按顺序 `InitInstance → Register`，把所有注册表和工厂装配好。后续代码只需 `StateRegisters.Instance.GetState("Walk")` 按名取用。

> 「服务定位器（Service Locator）+ 手动依赖注入」的落地，没有用第三方 DI 框架；`[SerializeReference]` 让基类数组能在 Inspector 里填任意子类，是实现多态注册的关键。

**代码**：`Assets/Scripts/System/Registers/*`、`Assets/Scripts/System/GameManager.cs`

### 6. 武器抽象 — Deployer / Projectile / Jointer + 对象池

武器（发射器）、弹药（抛射物）、挂点（炮塔）三层抽象，让「加一种武器」变成组合这三者。

- **`BaseDeployer`（发射器）**：管理发射冷却（`canLaunch` + 计时）、从对象池取弹、初始化弹药（`DeployerContext` 数据袋）、施加后坐力。
- **`BaseProjectile`（抛射物）**：持有方向 + 上下文，通过标记接口跑帧逻辑，碰撞后回池。
- **`Jointer`（挂点）**：坦克上的武器槽，`UpLoad` 装载发射器、`Launch` 触发发射，用 C# `event` 暴露 `launchEvent` / `switchEvent` 供 UI 等观察。
- **`ObjectPool<T>`**：泛型对象池（Queue），`ICanMoveToPool` 约束 Enable / Disable，弹药频繁生成销毁零 Instantiate。

> 三层职责清晰 + 模板方法（`BaseDeployer.Launch` 固定流程、子类只填 `Generate` / `InitContext` / `ReTime` 等钩子）+ 对象池优化。

**代码**：`Assets/Scripts/System/GameplaySystem/*`、`Assets/Scripts/Model/PlayerModel/Deployers/CannonDeployer/CannonDeployer.cs`、`Assets/Scripts/Tools/ObjectPool.cs`

---

## 设计理念

1. **一切行为皆是数据** —— 状态、转换、输入绑定都用 ScriptableObject / JSON 描述，引擎只管解析执行；
2. **一切耦合皆走约定** —— 状态只认 Context 数据袋，输入只走广播-队列-分发，模块只靠注册表按名取用；
3. **一切高频皆走复用** —— 输入用环形缓冲零 GC，弹药走对象池零 Instantiate。

核心目标：**让「加一个新状态 / 新武器 / 新输入」只做加法，不改主流程；同时把框架和内容分开，框架可复用。**

---

## 目录结构

```
Assets/
├── Scenes/
│   ├── Gameplay.unity          # 主场景（Build Settings 默认）
│   └── OpenMenu.unity          # 主菜单
├── Scripts/
│   ├── System/                 # 引擎层（50）：StateSystem / InputSystem / Registers
│   │                           #   / Factories / GameplaySystem / InventorySystem
│   ├── Model/                  # 内容层（35）：StateModel / InputModel / PlayerModel
│   │                           #   / MenuModel / SystemPrefabs
│   ├── View/                   # 表现层（7）：UIScripts / AudioScript
│   ├── Tools/                  # 工具（10）：ObjectPool / CircularArray
│   └── MainMenu/               # 菜单入口
├── Res/                        # Prefab、ScriptableObject、JSON 配置
├── Settings/                   # URP 渲染配置
└── Audio/                      # 音效（见下方说明）
Packages/manifest.json
ProjectSettings/
```

---

## 如何运行

1. 安装 **Unity 2022.3.57f1c1**（Unity 中国版；其他 2022.3 LTS 小版本通常也可，Unity 会提示升级）。
2. 用 Unity Hub 添加本仓库根目录并打开。首次打开会重新生成 `Library/`，需要几分钟。
3. 打开 `Assets/Scenes/Gameplay.unity`，按 Play 运行。

> **关于音频**：源工程的 `Assets/Audio/` 有 716 个素材文件、共 447 MB，但只有 **10 个**被场景和脚本真正引用。仓库出于体积和授权考虑，只纳入了这 10 个必需文件（46 MB），其余未提交。**不影响项目运行。**

---

## 第三方依赖

| 依赖 | 用途 | 来源 |
|---|---|---|
| Universal RP 14.0.11 | 渲染管线 | Unity Package Manager |
| Input System 1.11.2 | 输入 | Unity Package Manager |
| TextMesh Pro 3.0.7 | 文本渲染 | Unity Package Manager |
| Timeline 1.7.6 | 时间轴 | Unity Package Manager |

`Packages/manifest.json` 中列出了全部包依赖，Unity 打开工程时会自动还原。
