using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMenu : BaseMenu
{
    private float timeScale;
    protected override void ActWhenClose()
    {
        Time.timeScale = timeScale;
    }

    protected override void ActWhenOpen()
    {
        this.timeScale = Time.timeScale;
        Time.timeScale = 0;
    }
}
