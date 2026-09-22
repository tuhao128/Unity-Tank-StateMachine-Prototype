using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LogAction : BaseAction, IFixedUpdate1<InputAction.CallbackContext>
{
    public LogAction(bool shouldFixed) : base(shouldFixed)
    {
        
    }

    public void IFixedUpdate(InputAction.CallbackContext obj)
    {
        Debug.Log("kaishi" + this);//这个会产生少量gc
    }

    protected override bool JudgeFixed()
    {
        return shouldFixed;
    }
}
