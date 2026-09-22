using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanMoveToPool : IEnable, IDisable
{
    /// <summary>
    /// 挪至对象池，结尾调用一次
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    new void Disable();

    /// <summary>
    /// 从对象池取出，结尾调用一次
    /// </summary>
    new void Enable();
}
