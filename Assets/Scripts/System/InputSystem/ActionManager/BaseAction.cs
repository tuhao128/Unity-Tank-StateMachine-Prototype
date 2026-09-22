using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseAction
{
    protected bool shouldFixed;

    public BaseAction(bool shouldFixed)
    {
        this.shouldFixed = shouldFixed;
    }

    public bool ShouldFixed
    {
        get
        {
            return JudgeFixed();
        }
    }

    //判断是否需要放在fixed阶段使用
    protected abstract bool JudgeFixed();

}
