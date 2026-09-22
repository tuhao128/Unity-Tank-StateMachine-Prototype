using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OnExit", menuName = "State/BasicState/OnExit")]
public class OnExitState : BaseState, IFixedUpdate2<StateInformation, Context>
{
    public override void UpdateStateContext(ref StateContext context)
    {
        
    }

    public void IFixedUpdate(StateInformation t, Context m)
    {
        UnityEngine.Object.Destroy(m.controlledObject);
    }
}
