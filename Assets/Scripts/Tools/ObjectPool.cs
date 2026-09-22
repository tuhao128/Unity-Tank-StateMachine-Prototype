using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : ICanMoveToPool
{
    private static Queue<T> pool = new Queue<T>();

    public static void RemoveToPool(T obj)
    {
        pool.Enqueue(obj);
        obj.Disable();
    }

    /// <summary>
    /// 对象池存在物体就返回true，不存在false
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="actionIfNull"></param>
    /// <returns></returns>
    public static bool GetFromPool(out T obj, Func<object> actionIfNull)
    {
        if (pool.Count == 0)
        {
            obj = (T)actionIfNull();
            return false;
        }
        else
        {
            obj = pool.Dequeue();
            obj.Enable();
            return true;
        }
    }
}
