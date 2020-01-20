using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UiSoundCheckBox : MonoBehaviour
{
    [Inject]
    private SaveLoadScript _saveLoadScript;
    [Inject]
    private SoundController _soundController;
    [Inject]
    private SignalManager _signalManager;


    public void UpdateCheckBox()
    {
        transform.GetComponent<Toggle>().SetIsOnWithoutNotify(_soundController.SoundCheck());
    }


    public void SwitchSounds()
    {
        _soundController.SoundSetActive(transform.GetComponent<Toggle>().isOn);
        _signalManager._signalBus.TryFire<AudioChange>();
    }

}
