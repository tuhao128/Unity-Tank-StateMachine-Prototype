using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum TimeState
{
    Performed,
    Canceled,
    Started
}
//系统目标：解耦逻辑包和发包信息以及处理包的逻辑

//自身目标：
//         主要作用：在最开始的时候还有后续动态逻辑中，绑定Logic发送逻辑和Input操作
//         开始的绑定完全交给静态构造函数
//         至于asset，就创建一个Controls的单例吧，直接用unity的数据

//         开放一个api负责让存放数据的逻辑订阅事件，维护controls
//         存入缓存区的逻辑完全放在update之前进行，缓存区位于InputData内
//         InputData在update阶段从缓冲区获取数据，然后放入fixedupdate处理区
//外界依赖：LogicModle直接订阅这里的事件，LogicModle将会利用它向InputData发布Logic包
//外界限制：事件的触发交给InputManager，负责作为组件存入InputManager
public class ControlsManager
{
    //public void Clear()
    //{
        //actions.Clear();
    //}

    //string key;
    public static void InitControlsManager()
    {
        instance = new ControlsManager();
    }

    //private Dictionary<string, InputAction> actions = new Dictionary<string, InputAction>();

    private static ControlsManager instance;

    public static ControlsManager Instance
    {
        get
        {
            return instance;
        }
    }

    //public void RegisterAction(InputAction action)
    //{
        //actions.Add(action.actionMap.asset.GetInstanceID() + "_" + action.actionMap.name + "_" + action.name, action);
    //}

    public void RegisterLogic(InputAction action, Action<InputAction.CallbackContext> handler, TimeState time)
    {
        //key = action.actionMap.asset.GetInstanceID() + "_" + action.actionMap.name + "_" + action.name;
        switch (time)
        {
            case TimeState.Performed:
                action.performed += handler;
                break;
            case TimeState.Canceled:
                action.canceled += handler;
                break;
            case TimeState.Started:
                action.started += handler;
                break;
            default:
                break;
        }
    }
}
