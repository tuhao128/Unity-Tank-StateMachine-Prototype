using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct FallingCondition : ICondition<bool>
{
    private bool canUse;

    private bool isFalling;
    public bool CanUse { get => canUse; set => canUse = value; }
    public bool ConditionDetail { get => isFalling; set => isFalling = value; }
}

[CreateAssetMenu(fileName = "FallingCondSender", menuName = "CondSender/FallingCondSender")]
public class FallingCondSender : BaseConditionSender
{
    public override void SendCondition<T>(T data, BaseStateMachine stateMachine)
    {
        Condition condition = new Condition();
        FallingCondition fallingCondition = new FallingCondition();
        fallingCondition.CanUse = true;
        bool flag = RedefineData<bool>(data);
        fallingCondition.ConditionDetail = flag;
        condition.fallingCondition = fallingCondition;
        stateMachine.Switch(ref condition);
    }
}
