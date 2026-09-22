using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Input", menuName = "Premise/Input")]
public class InputPremise : BasePremise
{
    public override bool ShouldSwitch(in TransitionInformation transitionInformation, in PremiseInformation premiseInformation, in Condition condition, in StateInformation information)
    {
        if (condition.inputCondition.CanUse)
        {
            return premiseInformation.intNum == condition.inputCondition.ActionType;
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
