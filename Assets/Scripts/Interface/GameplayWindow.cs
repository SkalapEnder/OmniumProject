using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayWindow : Window
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image healthEdge;
    [SerializeField] private Button PauseButton;

    [Space][SerializeField] 
    private Slider experienceSlider;

    [SerializeField] private TMP_Text experienceLevel;

    [Space][SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text coinsText;

    public override void Initialize()
    {
        PauseButton.onClick.AddListener(PauseGameHandler);
    }

    protected override void OpenStart()
    {
        base.OpenStart();
        var player = GameManager.Instance.CharacterFactory.PlayerCharacter;
        UpdateHealthVisual(player);
        player.LiveComponent.OnCharacterHealthChange += UpdateHealthVisual;

        experienceSlider.maxValue = 
            GameManager.Instance.LevelManager.selectedLevel.XPLevelMax;

        experienceLevel.text = "0";

        UpdateScore(GameManager.Instance.ScoreSystem.Coins);
        GameManager.Instance.ScoreSystem.OnSessionCoinsUpdated += UpdateScore;
        GameManager.Instance.ScoreSystem.OnSessionXPUpdated += UpdateXP;
        GameManager.Instance.ScoreSystem.OnSessionXPLevelUpdated += UpdateXPLevel;
    }

    protected override void CloseStart()
    {
        base.CloseStart();

        var player = GameManager.Instance.CharacterFactory.PlayerCharacter;
        if (player == null)
            return;

        player.LiveComponent.OnCharacterHealthChange -= UpdateHealthVisual;
        GameManager.Instance.ScoreSystem.OnSessionCoinsUpdated -= UpdateScore;
        GameManager.Instance.ScoreSystem.OnSessionXPUpdated -= UpdateXP;
        GameManager.Instance.ScoreSystem.OnSessionXPLevelUpdated -= UpdateXPLevel;
    }

    private void PauseGameHandler()
    {
        GameManager.Instance.PauseGame();
    }

    private void UpdateHealthVisual(Character character)
    {
        if (WindowAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            WindowAnimator.Play("Idle");
        }
        WindowAnimator.Play("Hit", 0, 0f);

        int health = (int) character.LiveComponent.Health;
        int healthMax = (int) character.LiveComponent.MaxHealth;
        healthText.text = health + " / " + healthMax;
        healthSlider.maxValue = healthMax;
        healthSlider.value = health;
    }

    private void UpdateScore(int scoreCount)
    {
        Debug.Log("New Score data: " + scoreCount);
        coinsText.text = scoreCount.ToString();
    }

    private void UpdateXP(int earnedXP)
    {
        Debug.Log("New XP data: " + earnedXP);
        experienceSlider.value += earnedXP;
    }

    private void UpdateXPLevel(int newLevel)
    {
        experienceLevel.text = newLevel.ToString();
    }

    private void Update()
    {
        float totalSeconds = GameManager.Instance.GameTimeSeconds;
        int minutes = (int) (totalSeconds / 60);
        int seconds = (int) (totalSeconds % 60);

        timerText.text = minutes + ":" + ((seconds < 10) ? "0" + seconds : seconds);
    }
}
