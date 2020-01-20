using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIController : MonoBehaviour
{
    [Inject]
    private UiEndGameController _uiEndGameController;
    [Inject]
    private UiGameController _uiGameController;
    [Inject]
    private UiMainController _uiMainController;
    [Inject]
    private UiScoreController _uiScoreController;
    [Inject]
    private GameController _gameController;

    private void Start()
    {
        _uiMainController.MainMenuPanelSetActive(true);
        _uiGameController.GamePanelSetActive(false);
        _uiEndGameController.EndGamePanelSetActive(false);
    }

    public void OnExitBtnClick()
    {
        _uiScoreController.DropScore();
        _uiEndGameController.EndGamePanelSetActive(false);
        _uiGameController.OnExitMainMenuClick();
        _uiMainController.MainMenuPanelSetActive(true);
        _gameController.EndGame();
    }

    public void OnStartBtnClick()
    {
        _uiScoreController.DropScore();
        _uiMainController.MainMenuPanelSetActive(false);
        _uiGameController.GamePanelSetActive(true);
        _gameController.Play();
    }

    public void OnEndGameMenuOpen()
    {
        _uiGameController.GamePanelSetActive(false);
        _uiEndGameController.EndGamePanelSetActive(true);
        _uiEndGameController.RefreshScores();
    }
}
