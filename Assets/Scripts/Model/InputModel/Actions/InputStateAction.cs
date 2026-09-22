using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

//这里的这个IFixedUpdate的逻辑一定要改一下，让它跟上
public class InputStateAction : BaseAction, IFixedUpdate1<InputCallBack>
{
    public InputStateAction(bool shouldFixed) : base(shouldFixed)
    {
    }

    public void IFixedUpdate(InputCallBack obj)
    {
        if (!obj.context.action.name.Equals("SwitchJoint") && !obj.context.action.name.Equals("Fire"))
        {
            CondSenderRegisters.Instance.GetCondition("InputCondSender").SendCondition<InputCallBack>(obj, MarioMachine.Instance);
        }
        else if (obj.context.action.name.Equals("Fire"))
        {
            CondSenderRegisters.Instance.GetCondition("InputCondSender").SendCondition<InputCallBack>(obj, JointsMachine.Instance);
        }
    }

    protected override bool JudgeFixed()
    {
        return true;
    }

}
