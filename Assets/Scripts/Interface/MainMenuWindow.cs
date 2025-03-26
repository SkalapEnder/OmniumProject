using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuWindow : Window
{
    [SerializeField] private TMP_Text playerScore;
    [SerializeField] private TMP_Text playerXPLevel;
    [SerializeField] private Slider playerXP;
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button optionsGameButton;
    [SerializeField] private Button shopGameButton;

    public override void Initialize()
    {
        //startGameButton.onClick.AddListener(StartGameHandler);
        optionsGameButton.onClick.AddListener(OpenOptionsHandler);
        shopGameButton.onClick.AddListener(OpenShopHandler);

        GameManager.Instance.ScoreSystem.OnXPUpdated += UpdateUserXP;
        GameManager.Instance.ScoreSystem.OnXPLevelUpdated += UpdateUserLevel;

        playerScore.text = GameManager.Instance.ScoreSystem.CoinsGlobal.ToString();
        playerXP.maxValue = GameManager.Instance.ScoreSystem.XPMaxGlobal;
        playerXP.value = GameManager.Instance.ScoreSystem.XPGlobal;
        playerXPLevel.text = GameManager.Instance.ScoreSystem.XPLevelGlobal.ToString();
        Debug.Log("User Level: " + GameManager.Instance.ScoreSystem.XPLevelGlobal + ", User XP: " + GameManager.Instance.ScoreSystem.XPGlobal);
    }

    protected override void OpenStart()
    {
        base.OpenStart();
        startGameButton.interactable = true;
        optionsGameButton.interactable = true;
        shopGameButton.interactable = true;

        playerScore.text = GameManager.Instance.ScoreSystem.CoinsGlobal.ToString();
        playerXP.maxValue = GameManager.Instance.ScoreSystem.XPMaxGlobal;
        playerXP.value = GameManager.Instance.ScoreSystem.XPGlobal;
        playerXPLevel.text = GameManager.Instance.ScoreSystem.XPLevelGlobal.ToString();
        Debug.Log("User Level: " + GameManager.Instance.ScoreSystem.XPLevelGlobal + ", User XP: " + GameManager.Instance.ScoreSystem.XPGlobal);
    }

    protected override void CloseStart()
    {
        base.CloseStart();
        startGameButton.interactable = false;
        optionsGameButton.interactable = false;
        shopGameButton.interactable = false;
    }

    private void OpenOptionsHandler()
    {
        GameManager.Instance.WindowsService.ShowWindow<OptionsWindow>(false);
    }

    private void OpenShopHandler()
    {
        GameManager.Instance.WindowsService.ShowWindow<ShopWindow>(false);
    }

    public void UpdateUserLevel(int newLevel)
    {
        playerXPLevel.text = newLevel.ToString();
    }

    public void UpdateUserXP(int newXP)
    {
        playerXP.value = newXP;
    }
}
