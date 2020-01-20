using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController
{
    public void SetPouseOn()
    {
        Time.timeScale = 0;
    }

    public void SetPouseOff()
    {
        Time.timeScale = 1;
    }

    public bool IsPaused()
    {
        if (Time.timeScale == 1)
            return false;
        else
            return true;
    }
}
