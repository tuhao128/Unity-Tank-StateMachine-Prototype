using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TransitionConfig
{
    public string signedName;
    public string baseName;
    public TransitionInforConfig inforConfig;
    public string toSignedName;
    public string toBaseName;
    public PremiseConfig[] premiseConfigs;
}
