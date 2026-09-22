using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public struct InputCondition : ICondition<InputCallBack>
{
    private int type;

    private bool canUse;

    private InputCallBack callbackContext;

    public InputCallBack ConditionDetail
    {
        get
        {
            return callbackContext;
        }
        set
        {
            this.callbackContext = value;
        }
    }

    public bool CanUse { get => canUse; set => canUse = value; }

    public int ActionType { get => type; set => type = value; }
}

[CreateAssetMenu(fileName = "InputCondSender", menuName = "CondSender/InputCondSender")]
public class InputCondSender : BaseConditionSender
{
    public override void SendCondition<T>(T data, BaseStateMachine stateMachine)
    {
        Condition condition = new Condition();
        InputCondition inputCondition = new InputCondition();
        inputCondition.CanUse = true;
        InputCallBack callBack = RedefineData<InputCallBack>(data);
        inputCondition.ConditionDetail = callBack;
        inputCondition.ActionType = SwitchToAction(callBack.context.action.name);
        condition.inputCondition = inputCondition;
        stateMachine.Switch(ref condition);
    }

    //这里注册完别忘了去InputPremise注册
    private int SwitchToAction(string actionName)
    {
        return InputRegisters.Instance.GetValueBy(actionName);
    }
}
