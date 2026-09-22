using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;

//自动激活失活的单例系统
public class InstanceManager
{
    public static void Clear()
    {
        instanceDatas.Clear();
    }

    public static List<IDisable> instanceDatas = new List<IDisable>();

    //让所有的instance对象完成创建单例同时注册到Manager里
    public static void InitInstances()
    {
        AddInstances();
    }

    private static void AddInstances()
    {
        CollectDatas(new PlayerControlsInstance());
    }

    private static void CollectDatas(BaseInstance instanceData)
    {
        instanceDatas.Add(instanceData);
    }

    public static void OnDisable()
    {
        foreach (var item in instanceDatas)
        {
            item.Disable();
        }
    } 
}
