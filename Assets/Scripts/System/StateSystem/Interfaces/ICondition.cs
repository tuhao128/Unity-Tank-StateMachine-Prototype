using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICondition<T>
{
    public bool CanUse
    {
        get;
        set;
    }

    T ConditionDetail
    {
        get;
        set;
    }
}
