using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRedefineInstance<R>
{
    R RedefinedInstance
    {
        get;
    }
}
