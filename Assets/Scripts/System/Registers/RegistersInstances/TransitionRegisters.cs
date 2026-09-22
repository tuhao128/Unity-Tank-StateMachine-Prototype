using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionRegisters : MonoBehaviour
{
    private static TransitionRegisters instance;

    public static TransitionRegisters Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeReference]
    public BaseTransition[] transitions;

    private Dictionary<string, BaseTransition> keyValuePairs = new Dictionary<string, BaseTransition>();

    public void Register()
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            keyValuePairs.Add(transitions[i].BaseName, transitions[i]);
        }
    }

    public BaseTransition GetTransition(string baseName)
    {
        return keyValuePairs[baseName];
    }

    private void OnDisable()
    {
        keyValuePairs.Clear();
    }

    public void InitInstance()
    {
        instance = this;
    }
}
