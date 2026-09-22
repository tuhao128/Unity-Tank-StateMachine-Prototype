using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Pipeline;
using UnityEngine;

public class LogBC : BaseBroadcaster
{
    private BaseAction[] actions = new BaseAction[] { new LogAction(true) };

    public override BaseAction[] DefaultInstances
    {
        get
        {
            return actions;
        }
    }

    protected override void Registers()
    {
        Register(TimeState.Performed, PlayerControlsInstance.Instance.RedefinedInstance.PlayerMap.Jump);
    }
}
