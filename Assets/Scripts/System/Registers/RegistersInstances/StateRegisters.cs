using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateRegisters : MonoBehaviour, IRegisters
{
    private static StateRegisters instance;

    public static StateRegisters Instance
    {
        get
        {
            return instance;
        }
    }

    public void InitInstance()
    {
        instance = this;
    }

    [SerializeReference]
    public BaseState[] states;

    private Dictionary<string, BaseState> keyValuePairs = new Dictionary<string, BaseState>();

    public void Register()
    {
        for (int i = 0; i < states.Length; i++)
        {
            keyValuePairs.Add(states[i].BaseName, states[i]);
        }
    }

    public BaseState GetState(string baseName)
    {
        return keyValuePairs[baseName];
    }
}
