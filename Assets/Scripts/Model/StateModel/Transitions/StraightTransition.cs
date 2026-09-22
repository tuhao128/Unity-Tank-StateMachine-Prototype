using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Straight", menuName = "Transition/Straight")]
public class StraightTransition : BaseTransition
{
    public override bool ShouldSwitch(TransitionInformation transitionInfor, Context context, StateInformation information)
    {
        if (transitionInfor.boolNum)
        {
            Debug.Log("good");
        }
        return true;
    }

    public override bool ShouldSwitch(TransitionInformation transitionInfor, Condition condition, StateInformation information)
    {
        return true;
    }
}
