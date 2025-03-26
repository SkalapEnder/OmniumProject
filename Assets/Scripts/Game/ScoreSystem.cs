using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem
{
    private const string GLOBAL_COINS = "global_coins";
    private const string GLOBAL_XP = "global_xp";
    private const string GLOBAL_XP_LEVEL = "global_xp_level";

    public event Action<int> OnCoinsUpdated;
    public event Action<int> OnSessionCoinsUpdated;

    public event Action<int> OnXPUpdated;
    public event Action<int> OnXPLevelUpdated;
    public event Action<int> OnSessionXPUpdated;
    public event Action<int> OnSessionXPLevelUpdated;

    private int coinsGlobal;
    private int coins;
    private int coinsMax;

    private int xpGlobal;
    private int xpLevelGlobal;
    private float xpMaxGlobal = 850;
    private float xpMultGlobal = 1f;

    private int xp;
    private float xpMax = 200;
    private float xpMult = 1f;
    private int xpLevel;

    public int CoinsGlobal => coinsGlobal;
    public int Coins => coins;
    public int CoinsMax => coinsMax;

    public int XPGlobal => xpGlobal;
    public int XPLevelGlobal => xpLevelGlobal;
    public int XPMaxGlobal => (int) (
        xpMaxGlobal * ( Math.Pow(1.2f, xpLevelGlobal) > 1 
        ? Math.Pow(1.2f, xpLevelGlobal) 
        : 1 ));
    public int XP => xp;
    public int XPLevel => xpLevel;

    public bool IsNewScoreRecorded { get; private set; }
    public bool IsNewLevelReached { get; private set; }
    public Level CurrentLevel { get; set; }

    public ScoreSystem() 
    {
        coinsGlobal = PlayerPrefs.GetInt(GLOBAL_COINS, 0);
        xpGlobal = PlayerPrefs.GetInt(GLOBAL_XP);
        xpLevelGlobal = PlayerPrefs.GetInt(GLOBAL_XP_LEVEL);

        for(int i = 0; i < xpLevelGlobal; i++)
        {
            xpMultGlobal *= 1.2f;
        }

        IsNewScoreRecorded = false;
    }

    public void StartGame()
    {
        coins = 0;
        coinsMax = CurrentLevel.MaxScore;

        xp = 0;
        xpMax = CurrentLevel.XPLevelMax;
        xpLevel = 0;

        IsNewScoreRecorded = false;
        IsNewLevelReached = false;
    }

    public void EndGameWin()
    {
        if (coins > coinsMax)
        {
            coinsMax = coins;
            SaveLevelMaxScore();
        }
        coinsGlobal += coins;
        xpGlobal += (int) (xpLevel * xpMax * 0.32f); // 80% / 5 = 16%

        if(xpGlobal >= xpMaxGlobal * xpMultGlobal)
        {
            xpLevel++;
            xpGlobal = 0;
            xpMultGlobal *= 1.2f;
            OnXPLevelUpdated?.Invoke(xpLevel);
            Debug.Log("New Global Level is Reached");
        }
        
        PlayerPrefs.SetInt(GLOBAL_XP, xpGlobal);
        PlayerPrefs.SetInt(GLOBAL_XP_LEVEL, xpLevelGlobal);
        PlayerPrefs.SetInt(GLOBAL_COINS, coinsGlobal);
        PlayerPrefs.Save();
        Debug.Log("You WIN!");
        OnXPUpdated?.Invoke(xpGlobal);
    }

    public void EndGameLoose()
    {
        xpGlobal += (int)(xpLevel * xpMax * 0.12f); // 30% / 5 = 6%
        if (xpGlobal >= xpMaxGlobal * xpMultGlobal)
        {
            xpLevel++;
            xpGlobal = 0;
            xpMultGlobal *= 1.2f;
            Debug.Log("New Global Level is Reached");
        }

        PlayerPrefs.SetInt(GLOBAL_XP, xpGlobal);
        PlayerPrefs.SetInt(GLOBAL_XP_LEVEL, xpLevelGlobal);
        PlayerPrefs.Save();
    }

    public void CharacterDeathHandler(Character character) 
    {
        coins += character.CharacterData.ScoreCost;
        OnCoinsUpdated?.Invoke(coins);
        if (coins <= coinsMax) return;

        coinsMax = coins;
        SaveLevelMaxScore();
    }

    public void AddScore(int earnedScore)
    {
        coins += earnedScore;
        OnSessionCoinsUpdated?.Invoke(Coins);

        if (coins <= coinsMax) return;

        coinsMax = coins;
        SaveLevelMaxScore();
        IsNewScoreRecorded = true;
    }

    public void AddXP(int earnedXP)
    {
        xp += earnedXP;
        OnSessionXPUpdated?.Invoke(earnedXP);
        
        if(xp >= xpMax * xpMult)
        {
            xpLevel++;
            xp = 0;
            xpMax *= xpMult;
            xpMult += CurrentLevel.XPLevelMultiAdd;

            OnSessionXPUpdated?.Invoke(-10000);
            OnSessionXPLevelUpdated?.Invoke(xpLevel);
        }
    }

    public void ClearProgress()
    {
        PlayerPrefs.SetInt(GLOBAL_COINS, 0);
        PlayerPrefs.SetInt(GLOBAL_XP, 0);
        PlayerPrefs.SetInt(GLOBAL_XP_LEVEL, 0);
        PlayerPrefs.Save();

        coinsGlobal = 0;
        xpGlobal = 0;
        xpLevelGlobal = 0;
        xpMultGlobal = 1f;

        OnXPUpdated?.Invoke(0);
        OnXPLevelUpdated?.Invoke(0);
        Debug.Log("User progress is deleted!");
    }

    public void SaveLevelMaxScore()
    {
        CurrentLevel.setNewScore(coinsMax);
        PlayerPrefs.SetInt(CurrentLevel.LevelWorkName + "_max_coins", coinsMax);
        PlayerPrefs.Save();
    }
}
