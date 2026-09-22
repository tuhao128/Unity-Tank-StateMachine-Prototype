using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointsUIManager : MonoBehaviour
{
    [SerializeField]
    private JointsManager manager;

    private Jointer[] jointers;

    [SerializeField]
    private JointsUI[] jointsUIs;

    public void Init()
    {
        jointers = manager.GetJointerArray();
        for (int i = 0; i < jointsUIs.Length; i++)
        {
            jointsUIs[i].Jointer = jointers[i];
            jointsUIs[i].Subscribes();
            jointsUIs[i].JointerNum = i;
            jointsUIs[i].JointsManager = this.manager;
        }
    }
}
