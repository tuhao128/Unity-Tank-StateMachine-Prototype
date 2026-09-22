using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondSenderRegisters : MonoBehaviour
{
    private static CondSenderRegisters instance;

    public static CondSenderRegisters Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeReference]
    public BaseConditionSender[] conditionSenders;

    private Dictionary<string, BaseConditionSender> keyValuePairs = new Dictionary<string, BaseConditionSender>();

    public void Register()
    {
        for (int i = 0; i < conditionSenders.Length; i++)
        {
            keyValuePairs.Add(conditionSenders[i].BaseName, conditionSenders[i]);
        }
    }

    public BaseConditionSender GetCondition(string baseName)
    {
        return keyValuePairs[baseName];
    }

    public void InitInstance()
    {
        instance = this;
    }
}
