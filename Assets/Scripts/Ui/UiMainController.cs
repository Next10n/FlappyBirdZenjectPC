using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

public class UiMainController : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _scoreMenuPanel;
    [SerializeField] private GameObject _optionsMenuPanel;
    [SerializeField] private GameObject _hightScore;

    [Inject]
    private ScoreController _scoreController;

    [Inject]
    private UiSoundCheckBox _uiSoundController;


#if UNITY_EDITOR
    private void OnValidate()
    {
        _mainMenuPanel = gameObject;
        _scoreMenuPanel = transform.Find("ScoreMenu").gameObject;
        _optionsMenuPanel = transform.Find("OptionsMenu").gameObject;            
    }
#endif


    private void Start()
    {
        MainMenuPanelSetActive(true);
        ScorePanelSetActive(false);
        OptionsPanelSetActive(false);
    }

    public void MainMenuPanelSetActive(bool b) { _mainMenuPanel.SetActive(b); }
    public void ScorePanelSetActive(bool b) { _scoreMenuPanel.SetActive(b); }
    public void OptionsPanelSetActive(bool b) { _optionsMenuPanel.SetActive(b); }

    public void OnStartBtnClicked()
    {       
        MainMenuPanelSetActive(false);
    }

    public void OnScoreBtnClicked()
    {
        ScorePanelSetActive(true);
        RefreshScores();
    }

    public void OnScoreBtnCloseClicked()
    {
        _uiSoundController.UpdateCheckBox();
        ScorePanelSetActive(false);
    }

    public void OnOptionsBtnClicked()
    {
        _uiSoundController.UpdateCheckBox();
        OptionsPanelSetActive(true);
    }

    public void RefreshScores()
    {
        _hightScore.GetComponent<Text>().text = _scoreController.GetMaxScore().ToString();

    }

    public void OnOptionsBtnCloseClicked()
    {
        OptionsPanelSetActive(false);
    }


}
