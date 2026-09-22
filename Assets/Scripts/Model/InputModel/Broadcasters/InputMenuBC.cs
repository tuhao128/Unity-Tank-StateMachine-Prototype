using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InputMenu", menuName = "Broadcaster/InputMenu")]
public class InputMenuBC : BaseBroadcaster
{
    public override BaseAction[] DefaultInstances => new BaseAction[] { new InputMenuAction(false) };

    protected override void Registers()
    {
        Register(TimeState.Performed, PlayerControlsInstance.Instance.RedefinedInstance.MenuMap.StopMenu);
    }
}
