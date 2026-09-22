using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Input", menuName = "Transition/Input")]
public class InputTransition : BaseTransition
{
    public override bool ShouldSwitch(TransitionInformation transitionInfor, Context context, StateInformation information)
    {
        return false;
    }

    public override bool ShouldSwitch(TransitionInformation transitionInfor, Condition condition, StateInformation information)
    {
        if (!condition.inputCondition.CanUse)
        {
            return false;
        }
        else
        {
            TimeState timeState = (TimeState)transitionInfor.intNum;
            switch (timeState)
            {
                case TimeState.Performed:
                    return condition.inputCondition.ConditionDetail.time == CallBackTime.Performed;
                case TimeState.Canceled:
                    return condition.inputCondition.ConditionDetail.time == CallBackTime.Canceled;
                case TimeState.Started:
                    return condition.inputCondition.ConditionDetail.time == CallBackTime.Started;
                default:
                    return false;
            }
        }
    }
}
