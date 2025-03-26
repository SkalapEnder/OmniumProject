using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [Header("Import")]
    [SerializeField] private Sprite levelImage;
    [SerializeField] private GameData levelData;
    [SerializeField] private TMP_Text levelLabel;
    [SerializeField] private TMP_Text levelMaxScore;
    [SerializeField] private Button levelButton;

    [Space(10), Header("Settings")]
    [SerializeField] private string Name;
    [SerializeField] private string WorkName;
    [SerializeField] private int xpLevelToUnlock;
    [SerializeField] private int xpLevelMax = 100;
    [SerializeField] private float xpLevelMultiAdd = 0.1f;

    private int maxScore = 0;
    private bool isUnlocked = false;

    
    public int MaxScore => maxScore;
    public int XPLevelToUnlock => xpLevelToUnlock;
    public int XPLevelMax => xpLevelMax;
    public float XPLevelMultiAdd => xpLevelMultiAdd;
    public bool IsUnlocked => isUnlocked;

    public string LevelName => Name;
    public string LevelWorkName => WorkName;
    public GameData LevelData => levelData;
    public Sprite LevelImage => levelImage;

    public void Initialize()
    {
        maxScore = PlayerPrefs.GetInt(LevelWorkName + "_max_coins", 0);
        levelButton.onClick.AddListener(OnStartButtonPressed);
        levelLabel.text = Name;
        levelMaxScore.text = "Max Score: " + maxScore;
        isUnlocked = false;
    }
   

    public void setNewScore(int newScore)
    {
        maxScore = newScore;
    }

    public void checkPlayerMaxScore()
    {
        float playerLevel = GameManager.Instance.ScoreSystem.XPLevelGlobal;
        if (playerLevel >= xpLevelToUnlock) 
        {
            isUnlocked = true;
            levelButton.interactable = true;
            levelButton.GetComponentInChildren<TMP_Text>().text = "Start";
        } 
        else
        {
            isUnlocked = false;
            levelButton.interactable = false;
            levelButton.GetComponentInChildren<TMP_Text>().text = "Level " + xpLevelToUnlock ;
        }
    }

    private void OnStartButtonPressed()
    {
        GameManager.Instance.LevelManager.selectedLevel = this;
        GameManager.Instance.StartGame();
        GameManager.Instance.WindowsService.ShowWindow<GameplayWindow>(false);
    }
}
