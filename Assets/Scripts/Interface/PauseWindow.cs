using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseWindow : Window
{
    [Header("Buttons")]
    [SerializeField] private Button continuePlayButton;
    [SerializeField] private Button stopGameButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button soundHandlerButton;

    [Space(10), Header("Sound")]
    [SerializeField] private Image soundIcon;
    [SerializeField] private Sprite soundOnSprite;  // Icon when sound is enabled
    [SerializeField] private Sprite soundOffSprite; // Icon when sound is disabled

    public override void Initialize()
    {
        continuePlayButton.onClick.AddListener(OnContinuePlayButtonHandler);
        stopGameButton.onClick.AddListener(OnReturnToMainMenuButtonClicked);
        optionsButton.onClick.AddListener(OnOptionsButtonHandler);
        soundHandlerButton.onClick.AddListener(OnSoundChangeButtonHandler);
    }

    private void OnContinuePlayButtonHandler()
    {
        Hide(true);
        GameManager.Instance.PauseGame();
    }

    private void OnReturnToMainMenuButtonClicked()
    {
        Hide(true);
        GameManager.Instance.StopGame();
        GameManager.Instance.PauseGame();
        GameManager.Instance.WindowsService.ShowWindow<MainMenuWindow>(false);
    }

    private void OnOptionsButtonHandler() 
    {
        GameManager.Instance.WindowsService.ShowWindow<OptionsWindow>(false);
    }

    private void OnSoundChangeButtonHandler() 
    {
        GameManager.Instance.AudioService.ToggleMute();
        soundIcon.sprite = GameManager.Instance.AudioService.IsMasterEnabled ? soundOnSprite : soundOffSprite;
    }
}
