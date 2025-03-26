using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LooseWindow : Window
{
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Button mainMenuButton;

    public override void Initialize()
    {
        restartButton.onClick.AddListener(OnRestartButtonClicked);
        mainMenuButton.onClick.AddListener(OnReturnToMainMenuButtonClicked);
    }

    private void OnRestartButtonClicked()
    {
        Hide(true);
        GameManager.Instance.StartGame();
        GameManager.Instance.WindowsService.ShowWindow<GameplayWindow>(false);
    }

    private void OnReturnToMainMenuButtonClicked()
    {
        Hide(true);
        GameManager.Instance.LeaveGame();
        GameManager.Instance.WindowsService.ShowWindow<MainMenuWindow>(false);
    }    
}
