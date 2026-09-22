using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//这个instance系统改一改说不定还能用在其他地方上
//这个系统代表了自带激活失活和初始化的单例系统
public abstract class BaseInstance : ScriptableObject, IDisable, IAntoherInstance<IInputActionCollection2>
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


    public abstract IInputActionCollection2 DefaultInstance { get; }

    public void Init()
    {
        InitInstance();
    }

    protected abstract void InitInstance();

    protected abstract void OverDisable();

    public void Disable()
    {
        OverDisable();
    }
}
