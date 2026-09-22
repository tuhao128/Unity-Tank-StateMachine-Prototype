using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Falling", menuName = "Premise/Falling")]
public class FallingPremise : BasePremise
{
    public override bool ShouldSwitch(in TransitionInformation transitionInformation, in PremiseInformation premiseInformation, in Condition condition, in StateInformation information)
    {
        if (condition.fallingCondition.CanUse)
        {
            return condition.fallingCondition.ConditionDetail;
        }
        else
        {
            return false;
        }
    }

    public override bool ShouldSwitch(in TransitionInformation transitionInformation, in PremiseInformation premiseInformation, in Context context, in StateInformation information)
    {
        return false;
    }
}
