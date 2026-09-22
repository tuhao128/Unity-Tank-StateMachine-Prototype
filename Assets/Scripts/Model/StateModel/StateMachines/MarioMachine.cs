using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioMachine : BaseStateMachine
{
    [SerializeField]
    private PlayerAudios m_AudioSource;

    [SerializeField]
    private LayerMask groundMask;

    private static MarioMachine instance;

    public static MarioMachine Instance
    {
        get
        {
            return instance;
        }
    }

    public float fallSpeed;

    public float walkSpeed;

    public float jumpHeight;

    public float rollSpeed;

    protected override void InitMachine()
    {
        instance = this;
    }

    protected override void InitContext()
    {
        context.fixedUpdateFloatDetails.TryAdd("moveDirection", 0);
        context.fixedUpdateFloatDetails.TryAdd("rollSpeed", rollSpeed);
        context.fixedUpdateFloatDetails.TryAdd("walkSpeed", walkSpeed);
        context.fixedUpdateFloatDetails.TryAdd("jumpHeight", jumpHeight);
        context.fixedUpdateFloatDetails.TryAdd("fallSpeed", fallSpeed);

        context.fixedUpdateEventDetails.TryAdd("jumpEvent", PlayJumpSound);

        context.updateFloatDetails.TryAdd("moveDirection", 0);
        context.updateFloatDetails.TryAdd("rollSpeed", rollSpeed);
        context.updateFloatDetails.TryAdd("walkSpeed", walkSpeed);
        context.updateFloatDetails.TryAdd("jumpHeight", jumpHeight);
    }

    protected override void UpdateCondition(ref Condition condition)
    {
        if (condition.inputCondition.CanUse)
        {
            condition.floatNum = PlayerControlsInstance.Instance.RedefinedInstance.PlayerMap.Walk.ReadValue<float>();
            context.fixedUpdateFloatDetails["moveDirection"] = condition.floatNum;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & groundMask.value) != 0 && clip.CurrentState.state.BaseName != "Fall" && clip.CurrentState.state.BaseName != "Jump")
        {
            CondSenderRegisters.Instance.GetCondition("FallingCondSender").SendCondition<bool>(true, this);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_AudioSource.PlayMoving();
    }

    protected override void UpdateContext(Dictionary<string, object> contextDetails, Dictionary<string, float> floatDetails, Dictionary<string, GameObject> objectDetails, Dictionary<string, Action> eventDetails)
    {
        
    }

    protected override void FixedUpdateContext(Dictionary<string, object> contextDetails, Dictionary<string, float> floatDetails, Dictionary<string, GameObject> objectDetails, Dictionary<string, Action> eventDetails)
    {
        
    }

    protected void PlayJumpSound()
    {
        m_AudioSource.PlayJump();
    }
}
