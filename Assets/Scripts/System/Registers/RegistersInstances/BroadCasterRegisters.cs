using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadCasterRegisters : MonoBehaviour, IRegisters
{
    private static BroadCasterRegisters instance;

    public static BroadCasterRegisters Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeReference]
    public BaseBroadcaster[] broadcasters;

    private Dictionary<string, BaseBroadcaster> keyValuePairs = new Dictionary<string, BaseBroadcaster>();

    public void Register()
    {
        for (int i = 0; i < broadcasters.Length; i++)
        {
            broadcasters[i].Init();
            keyValuePairs.Add(broadcasters[i].BaseName, broadcasters[i]);
        }
    }

    public BaseBroadcaster GetCaster(string baseName)
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
