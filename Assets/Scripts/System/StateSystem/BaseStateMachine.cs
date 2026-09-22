using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

//这个是状态机自带的接收逻辑
public struct Context
{
    public GameObject controlledObject;

    public Transform controlledTransform;

    public Collider2D controlledCollider2D;

    public Rigidbody2D controlledRigidbody2D;

    public Dictionary<string, object> updateContextDetails;

    public Dictionary<string, float> updateFloatDetails;

    public Dictionary<string, object> fixedUpdateContextDetails;

    public Dictionary<string, float> fixedUpdateFloatDetails;

    public Dictionary<string, GameObject> updateObjectDetails;

    public Dictionary<string, GameObject> fixedUpdateObjectDetails;

    public Dictionary<string, Action> updateEventDetails;

    public Dictionary<string, Action> fixedUpdateEventDetails;

    public StateContext stateContext;

    public void AddStateContext(StateContext context)
    {
        stateContext = context;
    }

    public void AddGameObject(GameObject gameObject)
    {
        controlledObject = gameObject;
        controlledTransform = gameObject.transform;
        gameObject.TryGetComponent<Collider2D>(out controlledCollider2D);
        gameObject.TryGetComponent<Rigidbody2D>(out controlledRigidbody2D);
    }

    public void AddUpdateDict(Dictionary<string, object> contextDetails, Dictionary<string, float> floatDetails, Dictionary<string, GameObject> objectDetails, Dictionary<string, Action> eventDetails)
    {
        this.updateContextDetails = contextDetails;
        this.updateFloatDetails = floatDetails;
        this.updateObjectDetails = objectDetails;
        this.updateEventDetails = eventDetails;
    }

    public void AddFixedUpdateDict(Dictionary<string, object> contextDetails, Dictionary<string, float> floatDetails, Dictionary<string, GameObject> objectDetails, Dictionary<string, Action> eventDetails)
    {
        this.fixedUpdateContextDetails = contextDetails;
        this.fixedUpdateFloatDetails = floatDetails;
        this.fixedUpdateObjectDetails = objectDetails;
        this.fixedUpdateEventDetails = eventDetails;
    }

    public void AddTransform(Transform transform)
    {
        this.controlledTransform = transform;
    }
}

public abstract class BaseStateMachine : MonoBehaviour
{
    //update会用到的环境信息
    protected Dictionary<string, object> updateContextDetails = new Dictionary<string, object>();

    protected Dictionary<string, float> updateFloatDetails = new Dictionary<string, float>();

    protected Dictionary<string, GameObject> updateObjectDetails = new Dictionary<string, GameObject>();

    protected Dictionary<string, Action> updateEventDetails = new Dictionary<string, Action>();

    //fixedupdate会用到的环境信息
    protected Dictionary<string, object> fixedUpdateContextDetails = new Dictionary<string, object>();

    protected Dictionary<string, float> fixedUpdateFloatDetails = new Dictionary<string, float>();

    protected Dictionary<string, GameObject> fixedUpdateObjectDetails = new Dictionary<string, GameObject>();

    protected Dictionary<string, Action> fixedUpdateEventDetails = new Dictionary<string, Action>();

    /// <summary>
    /// StateMachine当前拥有的State切片，State切片可以随意替换
    /// </summary>
    protected StateClip clip;

    /// <summary>
    /// 于Inspector界面可直接导入配置的窗口，处理时这个优先处理
    /// </summary>
    [SerializeField]
    protected string jsonConfig;//这个优先

    /// <summary>
    /// 在Inspector界面控制的配置管理，如果没有配置json文件，就调用这里
    /// </summary>
    [SerializeField]
    protected StateClipConfig config;

    /// <summary>
    /// 环境信息容器，只需维护一个，非必要不会复制一份
    /// </summary>
    protected Context context;

    private void Start()
    {
        InitConfig();
        context = new Context();
        context.AddGameObject(this.gameObject);
        context.AddUpdateDict(updateContextDetails, updateFloatDetails, updateObjectDetails, updateEventDetails);
        context.AddFixedUpdateDict(fixedUpdateContextDetails, fixedUpdateFloatDetails, fixedUpdateObjectDetails, fixedUpdateEventDetails);
        InitContext();
        InitMachine();
    }

    private void Update()
    {
        UpdateContext(updateContextDetails, updateFloatDetails, updateObjectDetails, updateEventDetails);
        if (clip != null)
        {
            clip.Update(ref context);
        }
    }

    private void FixedUpdate()
    {
        FixedUpdateContext(fixedUpdateContextDetails, fixedUpdateFloatDetails, fixedUpdateObjectDetails, fixedUpdateEventDetails);
        if (clip != null)
        {
            clip.FixedUpdate(ref context);
            clip.Switch(ref context);
        }
    }
    /// <summary>
    /// Context会在Start阶段自动创建，信息表和物体自动绑定，之后调用一次
    /// </summary>
    protected abstract void InitContext();

    /// <summary>
    /// 于Start方法最后阶段调用一次
    /// </summary>
    protected abstract void InitMachine();

    /// <summary>
    /// Update调用之前更新一次，用于更新两个表
    /// </summary>
    /// <param name="contextDetails"></param>
    /// <param name="floatDetails"></param>
    protected abstract void UpdateContext(Dictionary<string, object> contextDetails, Dictionary<string, float> floatDetails, Dictionary<string, GameObject> objectDetails, Dictionary<string, Action> eventDetails);

    /// <summary>
    /// FixedUpdate更新之前调用一次，用于更新两个信息表
    /// </summary>
    /// <param name="contextDetails"></param>
    /// <param name="floatDetails"></param>
    protected abstract void FixedUpdateContext(Dictionary<string, object> contextDetails, Dictionary<string, float> floatDetails, Dictionary<string, GameObject> objectDetails, Dictionary<string, Action> eventDetails);

    public void Switch(ref Condition condition)
    {
        condition.AddObject(this.gameObject);
        UpdateCondition(ref condition);
        clip.Switch(ref condition);
    }

    private void InitConfig()
    {
        if (!string.IsNullOrEmpty(jsonConfig))
        {
            StateClipConfig stateClipConfig = JsonUtility.FromJson<StateClipConfig>(jsonConfig);
            clip = StateClipFactory.Instance.CreateStateClip(stateClipConfig);
        }
        else
        {
            if (!config.IsEmpty())
            {
                clip = StateClipFactory.Instance.CreateStateClip(config);
            }
        }
    }

    /// <summary>
    /// condition发布的时候触发一次，回调condition信息
    /// </summary>
    /// <param name="condition"></param>
    protected abstract void UpdateCondition(ref Condition condition);
}
