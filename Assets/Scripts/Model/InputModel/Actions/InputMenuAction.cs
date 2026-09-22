using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMenuAction : BaseAction, IUpdate1<InputCallBack>
{
    public InputMenuAction(bool shouldFixed) : base(shouldFixed)
    {
    }

    public void IUpdate(InputCallBack obj)
    {
        MenuManager.Instance.Open(obj.context.action.name);
    }

    protected override bool JudgeFixed()
    {
        return false;
    }
}
