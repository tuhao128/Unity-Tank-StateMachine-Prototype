using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Roll", menuName = "State/OtherState/Roll")]
public class RollState : BaseState, IFixedUpdate2<StateInformation, Context>
{
    public void IFixedUpdate(StateInformation t, Context m)
    {
        m.controlledRigidbody2D.AddTorque(-m.fixedUpdateFloatDetails["rollSpeed"] * m.fixedUpdateFloatDetails["moveDirection"]);
    }

    public override void UpdateStateContext(ref StateContext context)
    {
        
    }
}
