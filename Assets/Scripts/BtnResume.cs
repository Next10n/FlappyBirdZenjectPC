using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BtnResume : MonoBehaviour
{
    [Inject]
    private GameConfig _gameConfig;

    private void Awake()
    {
        transform.GetComponent<Text>().text = "Продолжить за " + _gameConfig.PointsForResumeGame.ToString() + " набранных очков";
    }
}
