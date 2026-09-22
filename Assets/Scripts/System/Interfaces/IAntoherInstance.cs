using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAntoherInstance<T>
{
    T DefaultInstance
    {
        get;
    }
}
