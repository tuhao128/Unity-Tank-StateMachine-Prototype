using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogTestPremise : BasePremise
{
    public override bool ShouldSwitch(in TransitionInformation transitionInformation, in PremiseInformation premiseInformation, in Condition condition, in StateInformation information)
    {
        Debug.Log("Condition");
        return false;
    }

    public override bool ShouldSwitch(in TransitionInformation transitionInformation, in PremiseInformation premiseInformation, in Context context, in StateInformation information)
    {
        Debug.Log("Context");
        return true;
    }
}
