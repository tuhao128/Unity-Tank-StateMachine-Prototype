using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//在初始化方法里创建具体的broadcaster对象
//然后在GameManager里利用构造函数进行初始化
public class BroadcasterManager
{
    private static BroadcasterManager instance;

    public static BroadcasterManager Instance
    {
        get
        {
            return instance;
        }
    }

    public void InitRegisters()
    {
        new InputBC();
    }

    public static void InitInstance()
    {
        instance = new BroadcasterManager();
    }
}
