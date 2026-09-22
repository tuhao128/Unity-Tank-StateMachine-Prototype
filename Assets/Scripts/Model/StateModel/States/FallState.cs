using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fall", menuName = "State/OtherState/Fall")]
public class FallState : BaseState, IFixedUpdate2<StateInformation, Context>
{
    public void IFixedUpdate(StateInformation t, Context m)
    {
        m.controlledRigidbody2D.AddForce(new Vector2(m.fixedUpdateFloatDetails["fallSpeed"] * m.fixedUpdateFloatDetails["moveDirection"], 0));
    }

    public override void UpdateStateContext(ref StateContext context)
    {
        
    }
}
