using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryWindow : Window
{
    [Space]
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Button mainMenuButton;
    [SerializeField]
    private TMP_Text recordText;
    [SerializeField]
    private TMP_Text newRecordText;

    public override void Initialize()
    {
        base.Initialize();
        restartButton.onClick.AddListener(OnRestartButtonClickHandler);
        mainMenuButton.onClick.AddListener(OnReturnToMainMenuButtonClickHandler);
    }

    private void OnRestartButtonClickHandler()
    {
        Hide(true);
        GameManager.Instance.StartGame();
        GameManager.Instance.WindowsService.ShowWindow<GameplayWindow>(false);
        
    }

    private void OnReturnToMainMenuButtonClickHandler()
    {
        Hide(true);
        GameManager.Instance.LeaveGame();
        GameManager.Instance.WindowsService.ShowWindow<MainMenuWindow>(false);
    }

    protected override void OpenStart()
    {
        base.OpenStart();
        recordText.text = GameManager.Instance.ScoreSystem.Coins.ToString();
        newRecordText.gameObject.SetActive(GameManager.Instance.ScoreSystem.IsNewScoreRecorded);
    }
}
