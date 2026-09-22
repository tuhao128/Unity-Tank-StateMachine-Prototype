using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremisesManager
{
    private Dictionary<string, BasePremise> registers = new Dictionary<string, BasePremise>();

    private static PremisesManager instance;

    public static PremisesManager Instance
    {
        get
        {
            return instance;
        }
    }

    public static void InitInstance()
    {
        instance = new PremisesManager();
    }

    public void InitRegisters()
    {
        Register(new LogTestPremise(), "LogText");
        Register(new InputPremise(), "Input");
    }

    private void Register(BasePremise premise, string stateName)
    {
        registers.Add(premise.BaseName, premise);
    }

    public BasePremise GetPremise(string baseName)
    {
        return registers[baseName];
    }
}
