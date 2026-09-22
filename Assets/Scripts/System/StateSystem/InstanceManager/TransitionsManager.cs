using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionsManager
{
    private Dictionary<string, BaseTransition> registers = new Dictionary<string, BaseTransition>();

    private static TransitionsManager instance;

    public static TransitionsManager Instance
    {
        get
        {
            return instance;
        }
    }

    public static void InitInstance()
    {
        instance = new TransitionsManager();
    }

    public void InitRegisters()
    {
        Register(new StraightTransition(), "Straight");
        Register(new InputTransition(), "Input");
    }

    public BaseTransition GetTransition(string baseName)
    {
        return registers[baseName];
    }

    private void Register(BaseTransition transition, string transitionName)
    {
        transition.BaseName = transitionName;
        registers.Add(transition.BaseName, transition);
    }
}
