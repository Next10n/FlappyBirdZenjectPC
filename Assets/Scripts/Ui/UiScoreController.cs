using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

public class UiScoreController : MonoBehaviour
{
    [Inject]
    private ScoreController _scoreController;

    public void DropScore()
    {
        transform.GetComponent<Text>().text = "0";
    }

    public void UpdateScore()
    {
        transform.GetComponent<Text>().text = _scoreController.GetScore().ToString();
    }


}
