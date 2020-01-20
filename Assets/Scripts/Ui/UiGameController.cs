using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UiGameController : MonoBehaviour
{
    [SerializeField] private GameObject _gameMenuPanel;
    [SerializeField] private GameObject _pauseMenuPanel;


    [Inject]
    private GameController _gameController;




#if UNITY_EDITOR
    private void OnValidate()
    {
        _gameMenuPanel = gameObject;
        _pauseMenuPanel = transform.Find("PausePanel").gameObject;
    }
#endif




    private void Start()
    {
        GamePanelSetActive(false);
        PausePanelSetActive(false);

    }

    public void GamePanelSetActive(bool b) { _gameMenuPanel.SetActive(b); }
    public void PausePanelSetActive(bool b) { _pauseMenuPanel.SetActive(b); }



    public void OnPauseBtnClick()
    {
        _gameController.PauseGame();
        _pauseMenuPanel.SetActive(true);
    }

    public void OnResumeBtnClick()
    {
        GamePanelSetActive(true);
        _pauseMenuPanel.SetActive(false);
        _gameController.UnpauseGame();
    }



    public void OnExitMainMenuClick()
    {
        _pauseMenuPanel.SetActive(false);
        _gameMenuPanel.SetActive(false);
    }



}
