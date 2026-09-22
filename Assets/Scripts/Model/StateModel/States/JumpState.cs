using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Jump", menuName = "State/OtherState/Jump")]
public class JumpState : BaseState, IFixedUpdate2<StateInformation, Context>
{
    public void IFixedUpdate(StateInformation t, Context m)
    {
        m.controlledRigidbody2D.AddForce(Vector2.up * m.fixedUpdateFloatDetails["jumpHeight"], ForceMode2D.Impulse);
        if (m.fixedUpdateEventDetails.TryGetValue("jumpEvent", out Action action))
        {
            action?.Invoke();
        }
    }

    public override void UpdateStateContext(ref StateContext context)
    {
        
    }
}
