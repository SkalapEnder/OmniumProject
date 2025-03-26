using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level[] levels;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;
    [SerializeField] private RectTransform viewport;
    public Level selectedLevel {get; set;}

    public void Initialize()
    {
        foreach (var level in levels)
        {
            level.Initialize();
            level.checkPlayerMaxScore();
        }
    }

    public void Restart()
    {
        foreach (var level in levels) 
        {
            level.Initialize();
            level.checkPlayerMaxScore();
        }
    }

    public void ResetProgress()
    {
        foreach (var level in levels)
        {
            PlayerPrefs.SetInt(level.LevelWorkName + "_max_coins", 0);
        }
        Restart();
    }

    public void SetNewMaxScore(Level level, int newMaxScore) 
    {
        level.setNewScore(newMaxScore);
    }
}
