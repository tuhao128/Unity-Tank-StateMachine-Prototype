using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseCondition<C>
{
    C Condition
    {
        get;
        set;
    }
}
