using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ValueType
{
    None,
    Int,
    Float,
    Gameobject,
    Bool
}

//这个是每个状态自身会收集的数据，都是定向的，都是只能满足于一个目标
public struct StateContext
{
    public static StateContext defaultContext = new StateContext();

    public int intValue;

    public float floatValue;

    public GameObject objectValue;

    public bool boolValue;

    public void Clear()
    {
        this = defaultContext;
    }
}

/// <summary>
/// 要实现帧逻辑，必须继承I(Fixed)Update2<StateInformation, Context>
/// </summary>
public abstract class BaseState : ScriptableObject
{
    [SerializeField]
    protected string baseName;

    public string BaseName
    {
        get
        {
            return baseName;
        }
        set
        {
            baseName = value;
        }
    }

    public abstract void UpdateStateContext(ref StateContext context);
}
