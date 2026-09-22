using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointsMachine : BaseStateMachine
{
    [SerializeField]
    private PlayerAudios m_AudioSource;

    private static JointsMachine instance;
    public static JointsMachine Instance {  get { return instance; } }

    [SerializeField]
    private JointsManager jointsManager;

    protected override void InitContext()
    {
        fixedUpdateObjectDetails.TryAdd("jointsManager", jointsManager.gameObject);

        context.fixedUpdateEventDetails.TryAdd("fireEvent", PlayFireSound);
    }

    protected override void InitMachine()
    {
        instance = this;
    }

    protected override void UpdateCondition(ref Condition condition)
    {
        
    }

    public void UpLoad(GameObject gameObject)
    {
        if (jointsManager != null)
        {
            jointsManager.CurrentUpLoad(gameObject);
        }
    }

    protected override void UpdateContext(Dictionary<string, object> contextDetails, Dictionary<string, float> floatDetails, Dictionary<string, GameObject> objectDetails, Dictionary<string, Action> eventDetails)
    {
        
    }

    protected override void FixedUpdateContext(Dictionary<string, object> contextDetails, Dictionary<string, float> floatDetails, Dictionary<string, GameObject> objectDetails, Dictionary<string, Action> eventDetails)
    {
        
    }

    protected void PlayFireSound()
    {
        m_AudioSource.PlayFire();
    }
}
