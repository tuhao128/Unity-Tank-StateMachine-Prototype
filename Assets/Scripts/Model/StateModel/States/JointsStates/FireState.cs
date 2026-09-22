using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fire", menuName = "State/DeployerState/Fire")]
public class FireState : BaseState, IFixedUpdate2<StateInformation, Context>
{
    public void IFixedUpdate(StateInformation t, Context m)
    {
        GameObject jointsManager;
        m.fixedUpdateObjectDetails.TryGetValue("jointsManager", out jointsManager);
        if (jointsManager.GetComponent<JointsManager>().CurrentLaunch())
        {
            if (m.fixedUpdateEventDetails.TryGetValue("fireEvent", out Action action))
            {
                action?.Invoke();
            }
        }
    }

    public override void UpdateStateContext(ref StateContext context)
    {
        
    }
}
