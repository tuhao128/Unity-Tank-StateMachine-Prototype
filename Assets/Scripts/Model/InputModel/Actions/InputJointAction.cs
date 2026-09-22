using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputJointAction : BaseAction, IFixedUpdate1<InputCallBack>
{
    public InputJointAction(bool shouldFixed) : base(shouldFixed)
    {
    }

    public void IFixedUpdate(InputCallBack obj)
    {
        
    }

    protected override bool JudgeFixed()
    {
        return true;
    }
}
