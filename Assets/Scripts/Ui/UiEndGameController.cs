using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

public class UiEndGameController : MonoBehaviour
{
    [SerializeField] private GameObject _endGamePanel;
    [SerializeField] private GameObject _maxScore;
    [SerializeField] private GameObject _currentScore;

    [Inject]
    private GameConfig _gameConfig;

    [Inject]
    private ScoreController _scoreController;

    [Inject]
    private GameController _gameController;

    [Inject]
    private SignalManager _signalManager;

#if UNITY_EDITOR
    private void OnValidate()
    {
        _endGamePanel = gameObject;
        _maxScore = transform.Find("MaxScore").gameObject;
        _currentScore = transform.Find("CurrentScore").gameObject;
    }
#endif

    public void RefreshScores()
    {
        _maxScore.GetComponent<Text>().text = _scoreController.GetMaxScore().ToString();
        _currentScore.GetComponent<Text>().text = _scoreController.GetScore().ToString();
    }

    public void EndGamePanelSetActive(bool b)
    {
        _gameController.PauseGame();
        _endGamePanel.SetActive(b);
    }

    public void OnResumeBtnClick()
    {
        if(_scoreController.GetScore() >= _gameConfig.PointsForResumeGame)
        {
            _scoreController.DecreaseScore(_gameConfig.PointsForResumeGame);
            _signalManager._signalBus.TryFire<ResumeSignal>();
            _gameController.UnpauseGame();
            EndGamePanelSetActive(false);
        }
    }



}
