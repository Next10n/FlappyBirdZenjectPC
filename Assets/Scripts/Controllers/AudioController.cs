using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AudioController : MonoBehaviour
{


    public void VolumeChange(bool flag)
    {
        if(flag)
            transform.GetComponent<AudioSource>().volume = 0.25f;
        else
            transform.GetComponent<AudioSource>().volume = 0;
    }
}
