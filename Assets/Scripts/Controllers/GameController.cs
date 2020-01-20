using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    [Inject]
    private SignalManager _signalManager;

    [Inject]
    private PlayerController.PlayerFabrik _playerFabrik;

    [Inject]
    private SaveLoadScript _saveLoadScript;

    [Inject]
    private CollumnsController.Factory _collumnsFabrik;

    [Inject]
    private ScoreController _scoreController;

    [Inject]
    private UiScoreController _uiScoreController;

    [Inject]
    private UiEndGameController _uiEndGameController;

    [Inject]
    private UIController _uiController;

    [Inject]
    private TimeController _timeController;

    [Inject]
    private SoundController _soundController;

    [Inject]
    private AudioController _audioController;

    [Inject]
    private UiGameController _uiGameController;

    [Inject]
    private GameConfig _gameConfig;


    public GameObject Player;
    public GameObject[] Collumns;

    [SerializeField]
    private AudioSource _endGameSound;

    [SerializeField]
    private AudioSource _scoreSound;

    private void Awake()
    {
        _saveLoadScript.LoadGame();
        _audioController.VolumeChange(_soundController.SoundCheck());
        Action ChangeVolumeAction = () => _audioController.VolumeChange(_soundController.SoundCheck());
        _signalManager._signalBus.Subscribe<AudioChange>(ChangeVolumeAction);
    }

    private void Start()
    {

    }

    public void Play()
    {
        CreateColumns();
        _signalManager._signalBus.Subscribe<ResumeSignal>(OnContinueBtnClick);
        _signalManager._signalBus.Subscribe<ResumeSignal>(_uiScoreController.UpdateScore);        
        _signalManager._signalBus.Subscribe<ScoreSignal>(_scoreController.AddScore);        
        _signalManager._signalBus.Subscribe<ScoreSignal>(_uiScoreController.UpdateScore);
        _signalManager._signalBus.Subscribe<ScoreSignal>(PlayScoreSound);
        _signalManager._signalBus.Subscribe<GameEndSignal>(PlayEndGameSound);
        _signalManager._signalBus.Subscribe<GameEndSignal>(_uiController.OnEndGameMenuOpen);
        _signalManager._signalBus.Subscribe<GameEndSignal>(_timeController.SetPouseOn);
        UnpauseGame();
        CreatePlayer();

    }


    public void EndGame()
    {
        _saveLoadScript.SaveGame();
        _signalManager._signalBus.TryUnsubscribe<ResumeSignal>(OnContinueBtnClick);
        _signalManager._signalBus.TryUnsubscribe<ResumeSignal>(_uiGameController.OnPauseBtnClick);
        _signalManager._signalBus.TryUnsubscribe<ResumeSignal>(_uiGameController.OnResumeBtnClick);
        _signalManager._signalBus.TryUnsubscribe<ResumeSignal>(_uiScoreController.UpdateScore);
        _signalManager._signalBus.TryUnsubscribe<ScoreSignal>(_scoreController.AddScore);
        _signalManager._signalBus.TryUnsubscribe<ScoreSignal>(_uiScoreController.UpdateScore);
        _signalManager._signalBus.TryUnsubscribe<ScoreSignal>(PlayScoreSound);
        _signalManager._signalBus.TryUnsubscribe<GameEndSignal>(PlayEndGameSound);
        _signalManager._signalBus.TryUnsubscribe<GameEndSignal>(_uiController.OnEndGameMenuOpen);
        _signalManager._signalBus.TryUnsubscribe<GameEndSignal>(_timeController.SetPouseOn);        
        PlayerDestroy();
        CollumnsDestroy();
    }

    private void PlayScoreSound()
    {
        if(_soundController.SoundCheck())
            _scoreSound.Play();
    }

    private void PlayEndGameSound()
    {
        if (_soundController.SoundCheck())
            _endGameSound.Play();
    }

    private void PlayerDestroy()
    {
        Destroy(Player);
    }

    private void CollumnsDestroy()
    {
        foreach(GameObject column in Collumns)
            Destroy(column);
    }

    public void PauseGame()
    {
        _timeController.SetPouseOn();
    }

    public void UnpauseGame()
    {
        _timeController.SetPouseOff();
    }

    private void CreatePlayer()
    {
        Player = _playerFabrik.Create().gameObject;
    }

    public void OnContinueBtnClick()
    {
        _uiGameController.GamePanelSetActive(true);
        _uiGameController.PausePanelSetActive(true);
        //_gameController.UnpauseGame();
    }


    private void CreateColumns()
    {
        int j = (int)((_gameConfig.WallsStartPosition - _gameConfig.WallsLeftBorder) / _gameConfig.DistanceBetweenWalls);
        Collumns = new GameObject[j];
        for (int i = 0; i < j; i++)
        {
            Collumns[i] = _collumnsFabrik.Create(_gameConfig.WallsSpeed, _gameConfig.WallsLeftBorder, _gameConfig.WallsStartPosition).gameObject;
            Collumns[i].transform.position = new Vector3(_gameConfig.WallsStartPosition + i * _gameConfig.DistanceBetweenWalls,
                UnityEngine.Random.Range(_gameConfig.MinPassHieght, _gameConfig.MaxPassHieght),
                0);
        }        
       
    }

    private void OnApplicationQuit()
    {
        Action ChangeVolumeAction = () => _audioController.VolumeChange(_soundController.SoundCheck());
        _signalManager._signalBus.TryUnsubscribe<AudioChange>(ChangeVolumeAction);
        _saveLoadScript.SaveGame();
    }




}
