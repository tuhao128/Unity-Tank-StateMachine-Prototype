using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogState : BaseState, IUpdate2<StateInformation, Context>
{
    public override void UpdateStateContext(ref StateContext context)
    {
        context.floatValue = 10;
    }

    public void IUpdate(StateInformation t, Context m)
    {
        Debug.Log("成功执行动作");
        Debug.Log(m.fixedUpdateContextDetails["good"] + "_" + m.stateContext.floatValue);
        Debug.Log(t.doubleNum);
    }
}
