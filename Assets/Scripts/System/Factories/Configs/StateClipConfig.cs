using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StateClipConfig
{
    public string name;
    public StateConfig[] states;
    private StateConfig anyConfig = new StateConfig()
    {
        baseName = "Any"
    };
    public TransitionConfig[] anyStateTransitions;

    private StateConfig onEnterConfig = new StateConfig()
    {
        baseName = "OnEnter",
        shouldDefault = true
    };
    public TransitionConfig[] onEnterStateTransitions;

    private StateConfig onExitConfig = new StateConfig()
    {
        baseName = "OnExit"
    };
    public TransitionConfig[] onExitStateTransitions;

    public StateConfig OnEnterConfig
    {
        get
        {
            if (onEnterConfig.transitionConfigs == null)
            {
                onEnterConfig.transitionConfigs = onEnterStateTransitions;
            }
            return onEnterConfig;
        }
    }
    public StateConfig OnExitConfig
    {
        get
        {
            if (onExitConfig.transitionConfigs == null)
            {
                onExitConfig.transitionConfigs = onExitStateTransitions;
            }
            return onExitConfig;
        }
    }
    public StateConfig AnyConfig
    {
        get
        {
            if (anyConfig.transitionConfigs == null)
            {
                anyConfig.transitionConfigs = anyStateTransitions;
            }
            return anyConfig;
        }
    }

    public bool IsEmpty()
    {
        return states.Length == 0;
    }
}
