using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButtomLogic : MonoBehaviour
{
    public void Switch(int num, JointsManager manager)
    {
        manager.SwitchJoint(num);
    }
}
