using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTransition : ScriptableObject
{
    [SerializeField]
    protected string baseName;

    public string BaseName
    {
        get
        {
            return baseName;
        }
        set
        {
            baseName = value;
        }
    }

    //主动变换
    public bool ShouldSwitch(TransitionInformation transitionInfor, Condition condition, StateInformation information, List<Premise> premises)
    {
        if (!ShouldSwitch(transitionInfor, condition, information))
        {
            return false;
        }
        for (int i = 0; i < premises.Count; i++)
        {
            if (!premises[i].ShouldSwitch(transitionInfor, condition, information))
            {
                return false;
            }
        }
        return true;
    }

    //根据情景自动变换
    public bool ShouldSwitch(TransitionInformation transitionInfor, Context context, StateInformation information, List<Premise> premises)
    {
        if (!ShouldSwitch(transitionInfor, context, information))
        {
            return false;
        }
        for (int i = 0; i < premises.Count; i++)
        {
            if (!premises[i].ShouldSwitch(transitionInfor, context, information))
            {
                return false;
            }
        }
        return true;
    }

    public abstract bool ShouldSwitch(TransitionInformation transitionInfor, Context context, StateInformation information);

    public abstract bool ShouldSwitch(TransitionInformation transitionInfor, Condition condition, StateInformation information);
}
