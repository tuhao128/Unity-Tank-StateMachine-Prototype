using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesManager
{
    private Dictionary<string, BaseState> registers = new Dictionary<string, BaseState>();

    private static StatesManager instance;

    public static StatesManager Instance
    {
        get
        {
            return instance;
        }
    }

    public static void InitInstance()
    {
        instance = new StatesManager();
    }

    public void InitRegisters()
    {
        Register(new AnyState(), "Any");
        Register(new OnEnterState(), "OnEnter");
        Register(new OnExitState(), "OnExit");
        Register(new LogState(), "TestLog");
        Register(new WalkState(), "Walk");
        Register(new JumpState(), "Jump");
        Register(new IdleState(), "Idle");
    }

    private void Register(BaseState state, string stateName)
    {
        state.BaseName = stateName;
        registers.Add(state.BaseName, state);
    }

    public BaseState GetState(string baseName)
    {
        return registers[baseName];
    }
}
