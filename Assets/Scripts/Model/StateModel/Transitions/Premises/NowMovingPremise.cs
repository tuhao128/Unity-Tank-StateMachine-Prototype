using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NowMoving", menuName = "Premise/NowMoving")]
public class NowMovingPremise : BasePremise
{
    public override bool ShouldSwitch(in TransitionInformation transitionInformation, in PremiseInformation premiseInformation, in Condition condition, in StateInformation information)
    {
        return true;
    }

    public override bool ShouldSwitch(in TransitionInformation transitionInformation, in PremiseInformation premiseInformation, in Context context, in StateInformation information)
    {
        if (premiseInformation.boolNum)
        {
            return context.fixedUpdateFloatDetails["moveDirection"] != 0;
        }
        return false;
    }
}
