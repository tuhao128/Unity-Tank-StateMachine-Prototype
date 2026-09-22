using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControInstanceRegisters : MonoBehaviour
{
    private static ControInstanceRegisters instance;

    public static ControInstanceRegisters Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeReference]
    public BaseInstance[] instances;

    private Dictionary<string, BaseInstance> keyValuePairs = new Dictionary<string, BaseInstance>();

    public void Register()
    {
        for (int i = 0; i < instances.Length; i++)
        {
            instances[i].Init();
            keyValuePairs.Add(instances[i].BaseName, instances[i]);
        }
    }

    private void OnDisable()
    {
        foreach (var item in keyValuePairs.Values)
        {
            item.Disable();
        }
        keyValuePairs.Clear();
    }

    public BaseInstance GetInstance(string baseName)
    {
        return keyValuePairs[baseName];
    }

    public void InitInstance()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        foreach (var item in keyValuePairs.Values)
        {
            item.Disable();
        }
        keyValuePairs.Clear();
    }
}
