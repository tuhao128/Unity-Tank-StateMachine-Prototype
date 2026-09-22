using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRegisters : MonoBehaviour
{
    private Dictionary<string, int> namesValuesDic = new Dictionary<string, int>();

    public string[] namesToValues;

    private static InputRegisters instance;

    public static InputRegisters Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        instance.Init();
    }

    private void Init()
    {
        for (int i = 0; i < namesToValues.Length; i++)
        {
            namesValuesDic.Add(namesToValues[i], i);
        }
    }

    public int GetValueBy(string inputActionName)
    {
        return namesValuesDic[inputActionName];
    }

    private void OnDisable()
    {
        namesValuesDic.Clear();
    }
}
