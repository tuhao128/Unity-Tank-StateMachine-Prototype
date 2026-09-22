using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Input", menuName = "Broadcaster/Input")]
public class InputBC : BaseBroadcaster
{
    private BaseAction[] otherClassInstance = new BaseAction[]{ new InputStateAction(true) };
    public override BaseAction[] DefaultInstances { get => otherClassInstance; }

    protected override void Registers()
    {
        for (global::System.Int32 j = 0; j < PlayerControlsInstance.Instance.RedefinedInstance.asset.actionMaps[0].actions.Count; j++)
        {
            Register(TimeState.Performed, PlayerControlsInstance.Instance.RedefinedInstance.asset.actionMaps[0].actions[j]);
            Register(TimeState.Started, PlayerControlsInstance.Instance.RedefinedInstance.asset.actionMaps[0].actions[j]);
            Register(TimeState.Canceled, PlayerControlsInstance.Instance.RedefinedInstance.asset.actionMaps[0].actions[j]);
        }
    }
}
