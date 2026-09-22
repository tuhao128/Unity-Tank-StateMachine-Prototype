using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

[CreateAssetMenu(fileName = "Walk", menuName = "State/OtherState/Walk")]
public class WalkState : BaseState, IFixedUpdate2<StateInformation, Context>
{
    public void IFixedUpdate(StateInformation t, Context m)
    {
        m.controlledRigidbody2D.AddForce(new Vector2(m.fixedUpdateFloatDetails["walkSpeed"] * m.fixedUpdateFloatDetails["moveDirection"], 0));
    }

    public override void UpdateStateContext(ref StateContext context)
    {

    }
}
