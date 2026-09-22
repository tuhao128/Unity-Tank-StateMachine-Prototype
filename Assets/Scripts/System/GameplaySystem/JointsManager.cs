using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointsManager : MonoBehaviour
{
    [SerializeField]
    private Jointer[] jointers;

    private int currentNum;

    [SerializeField]
    private JointsUIManager jointsUImanager;

    private void Awake()
    {
        list = new List<Jointer>(4);
        for (int i = 0; i < jointers.Length; i++)
        {
            list.Add(jointers[i]);
        }
        SwitchJoint(currentNum);
        if (jointsUImanager != null)
        {
            jointsUImanager.Init();
        }
    }

    private List<Jointer> list;

    public void SwitchJoint(int num)
    {
        CurrentDeployer.Disselected();
        currentNum = num;
        CurrentDeployer.Selected();
    }

    public void CurrentUpLoad(GameObject go)
    {
        list[currentNum].UpLoad(go);
    }

    public Jointer CurrentDeployer
    {
        get
        {
            return list[currentNum];
        }
    }

    public bool CurrentLaunch()
    {
        if (CurrentDeployer != null)
        {
            return CurrentDeployer.Launch();
        }
        return false;
    }

    public Jointer[] GetJointerArray()
    {
        return list.ToArray();
    }
}
