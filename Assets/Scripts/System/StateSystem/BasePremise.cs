using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PremiseInformation
{
    public int intNum;

    public float floatNum;

    public bool boolNum;

    public double doubleNum;
}

public struct Premise
{
    public BasePremise premise;
    public PremiseInformation premiseInformation;

    public bool ShouldSwitch(in TransitionInformation transitionInformation, in Condition condition, in StateInformation information)
    {
        return premise.ShouldSwitch(transitionInformation, this.premiseInformation, condition, information);
    }
    public bool ShouldSwitch(in TransitionInformation transitionInformation, in Context context, in StateInformation information)
    {
        return premise.ShouldSwitch(transitionInformation, this.premiseInformation, context, information);
    }
}

public abstract class BasePremise : ScriptableObject
{
    [SerializeField]
    protected string baseName;

    public string BaseName
    {
        get
        {
            return baseName;
        }
    }

    public abstract bool ShouldSwitch(in TransitionInformation transitionInformation, in PremiseInformation premiseInformation, in Condition condition, in StateInformation information);
    public abstract bool ShouldSwitch(in TransitionInformation transitionInformation, in PremiseInformation premiseInformation, in Context context, in StateInformation information);
}
