using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondSendersManager
{
    private static CondSendersManager instance;

    public static CondSendersManager Instance
    {
        get
        {
            return instance;
        }
    }

    public static void InitInstance()
    {
        instance = new CondSendersManager();
    }

    public void InitRegisters()
    {
        new InputCondSender();
    }
}
