using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public enum CallBackTime
{
    None,
    Started,
    Performed,
    Canceled
}
public struct InputCallBack
{
    public InputAction.CallbackContext context;

    public CallBackTime time;

    public InputCallBack(InputAction.CallbackContext context, CallBackTime time)
    {
        this.context = context;
        this.time = time;
    }
}

public struct ActionStack : IEquatable<ActionStack>
{
    public BaseAction action;

    public InputCallBack context;

    public ActionStack(BaseAction action, InputCallBack context)
    {
        this.action = action;
        this.context = context;
    }

    public void Update()
    {
        if (action != null && action is IUpdate1<InputCallBack> update)
        {
            update.IUpdate(context);
        }
    }

    public void FixedUpdate()
    {
        if (action != null && action is IFixedUpdate1<InputCallBack> update)
        {
            update.IFixedUpdate(context);
        }
    }

    public bool Equals(ActionStack other)
    {
        return context.Equals(other.context) && action.GetType().Equals(other.GetType());
    }
}
//订阅操作写在构造函数里
//负责提供发包操作和维护内部的一个逻辑单例
//每次订阅后的发包操作为发布一个ActionStack，ActionStack包含了数据和数据编号以及action实例
//这俩的关系类似于Item和Itemstack

//发布包就是把对应actionstack的引用交给ActionManager的特定阶段，在ActionManager里执行

public abstract class BaseBroadcaster : ScriptableObject, IAntoherInstances<BaseAction>
{
    [SerializeField]
    protected string baseName;

    public string BaseName
    {
        get
        {
            return baseName;
        }
    }

    private List<InputAction> performedInputs = new List<InputAction>(100)
    {
        //所有要写的那些输入绑定写在这里
    };
    private List<InputAction> canceledInputs = new List<InputAction>(100)
    {
        //所有要写的那些输入绑定写在这里
    };
    private List<InputAction> startedInputs = new List<InputAction>(100)
    {
        //所有要写的那些输入绑定写在这里
    };

    protected bool isFixed;

    /// <summary>
    /// BC会发布的动作包的集合
    /// </summary>
    public abstract BaseAction[] DefaultInstances { get; }

    public void Init()
    {
        Registers();
        Subscribe();
    }

    //这个接口开放出来，给ControlsManager直接管理，属于外交内容
    //从对象池取出包的逻辑完全由它管理
    //然后action列表里存储的动作包，是action类别和编号，编号代表action执行状态

    /// <summary>
    /// 由Performed阶段的事件直接调用
    /// </summary>
    /// <param name="context"></param>
    public void SendPackagePerformed(InputAction.CallbackContext context)
    {
        RegAction(context, CallBackTime.Performed);
    }

    /// <summary>
    /// 由Started阶段的事件直接调用
    /// </summary>
    /// <param name="context"></param>
    public void SendPackageStarted(InputAction.CallbackContext context)
    {
        RegAction(context, CallBackTime.Started);
    }

    /// <summary>
    /// 由Canceled阶段的事件直接调用
    /// </summary>
    /// <param name="context"></param>
    public void SendPackageCanceled(InputAction.CallbackContext context)
    {
        RegAction(context, CallBackTime.Canceled);
    }

    /// <summary>
    /// 发出信息给缓存区
    /// </summary>
    /// <param name="context"></param>
    /// <param name="time"></param>
    public void RegAction(InputAction.CallbackContext context, CallBackTime time)
    {
        for (int i = 0; i < DefaultInstances.Length; i++)
        {
            isFixed = DefaultInstances[i].ShouldFixed;
            if (isFixed)
            {
                ActionManager.Instance.AddFixedUpdateAction(new ActionStack(DefaultInstances[i], new InputCallBack(context, time)));
            }
            else
            {
                ActionManager.Instance.AddUpdateAction(new ActionStack(DefaultInstances[i], new InputCallBack(context, time)));
            }
        }
    }

    /// <summary>
    /// 用于订阅
    /// </summary>
    private void Subscribe()
    {
        for (int i = 0; i < performedInputs.Count; i++)
        {
            ControlsManager.Instance.RegisterLogic(performedInputs[i], SendPackagePerformed, TimeState.Performed);
        }
        for (int i = 0; i < canceledInputs.Count; i++)
        {
            ControlsManager.Instance.RegisterLogic(canceledInputs[i], SendPackageCanceled, TimeState.Canceled);
        }
        for (int i = 0; i < startedInputs.Count; i++)
        {
            ControlsManager.Instance.RegisterLogic(startedInputs[i], SendPackageStarted, TimeState.Started);
        }
    }

    /// <summary>
    /// 这里把所有list里添加input操作写进去
    /// </summary>
    protected abstract void Registers();

    protected void Register(TimeState state, InputAction action)
    {
        switch (state)
        {
            case TimeState.Performed:
                performedInputs.Add(action);
                break;
            case TimeState.Canceled:
                canceledInputs.Add(action);
                break;
            case TimeState.Started:
                startedInputs.Add(action);
                break;
            default:
                break;
        }
    }
}
