using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JointsUI : MonoBehaviour
{
    private JointsManager jointsManager;

    public JointsManager JointsManager
    {
        set { jointsManager = value; }
    }

    private Jointer jointer;

    private int jointerNum;

    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    [SerializeField]
    private JointsUIManager jointsUIManager;

    public Jointer Jointer
    {
        get { return jointer; }
        set { jointer = value; }
    }

    public void Subscribes()
    {
        jointer.Subscribe(JointerEventType.SwitchEvent, Switch);
    }

    public void Switch(BaseDeployer deployer)
    {
        SwitchName(deployer.BaseName);
    }
    private void SwitchName(string name)
    {
        textMeshPro.text = name;
    }

    public void Switch()
    {
        jointsManager.SwitchJoint(jointerNum);
    }

    public int JointerNum
    {
        set {  jointerNum = value; }
    }
}
