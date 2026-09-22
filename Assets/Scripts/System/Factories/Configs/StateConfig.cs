using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StateConfig
{
    public string signedName;
    public string baseName;
    public bool shouldDefault;
    public StateInforConfig informationConfig;
    public TransitionConfig[] transitionConfigs;
}
