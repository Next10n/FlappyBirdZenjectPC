using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SoundController
{    

    private bool _soundActive = true;

    public void SoundSetActive(bool f)
    {
        _soundActive = f;
    }

    public bool SoundCheck()
    {
        return _soundActive;
    }
}
