using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

//用于发送切换逻辑
//每种类别的信息，都有一个Set和Get方法，这是稳定的
//外界只能使用Set和Get方法获取和改变信息

//具体如何维护信息，就是另外提的事情，但是对外开放的api一定要不变，必须一个get一个set
public struct Condition : IBaseCondition<InputCondition>, IBaseCondition<FallingCondition>
{
    public GameObject gameObject;

    public int intNum;

    public float floatNum;

    public bool boolNum;

    public double doubleNum;

    public void AddObject(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }

    public InputCondition inputCondition;

    InputCondition IBaseCondition<InputCondition>.Condition 
    {
        get
        {
            if (!inputCondition.CanUse)
            {
                Debug.LogWarning(inputCondition + " " + "now Cannot Be Used!");
            }
            return inputCondition;
        }
        set
        {
            this.inputCondition = value;
        }
    }

    public FallingCondition fallingCondition;

    FallingCondition IBaseCondition<FallingCondition>.Condition 
    {
        get
        {
            if (!fallingCondition.CanUse)
            {
                Debug.LogWarning(fallingCondition + " " + "now Cannot Be Used!");
            }
            return fallingCondition;
        }
        set
        {
            this.fallingCondition = value;
        }
    }
}

public abstract class BaseConditionSender : ScriptableObject
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

    public abstract void SendCondition<T>(T data, BaseStateMachine stateMachine);

    protected T RedefineData<T>(object data)
    {
        if(data is T newData)
        {
            return newData;
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning("输入类型错误！");
#endif
            return default;
        }
    }
}
