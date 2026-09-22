using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Pipeline;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerControls", menuName = "InputAssetInstance/PlayerControls")]
public class PlayerControlsInstance : BaseInstance, IRedefineInstance<PlayerControls>
{
    private static PlayerControlsInstance instance;

    public static PlayerControlsInstance Instance
    {
        get
        {
            return instance;
        }
    }

    private PlayerControls otherClassInstance;

    public override IInputActionCollection2 DefaultInstance
    {
        get
        {
            return otherClassInstance;
        }
    }

    public PlayerControls RedefinedInstance => otherClassInstance;

    protected override void InitInstance()
    {
        instance = this;
        instance.otherClassInstance = new PlayerControls();
        otherClassInstance.Enable();
    }

    protected override void OverDisable()
    {
        if (otherClassInstance != null)
        {
            otherClassInstance.Disable();
        }
    }
}
