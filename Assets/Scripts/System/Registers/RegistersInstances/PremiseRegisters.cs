using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremiseRegisters : MonoBehaviour
{
    private static PremiseRegisters instance;

    public static PremiseRegisters Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeReference]
    public BasePremise[] premises;

    private Dictionary<string, BasePremise> keyValuePairs = new Dictionary<string, BasePremise>();

    public void Register()
    {
        for (int i = 0; i < premises.Length; i++)
        {
            keyValuePairs.Add(premises[i].BaseName, premises[i]);
        }
    }

    public BasePremise GetPremise(string baseName)
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
