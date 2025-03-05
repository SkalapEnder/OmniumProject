using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ENDGAME endGame;
    [SerializeField] private HUD hud;
    [SerializeField] private PlayerCharacter player;
    private string currentSceneName;
    private bool isGameDone;

    private void Awake()
    {
        isGameDone = false;
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        if (!player.LiveComponent.IsAlive)
        {
            EndGame();
        }
        

        if (Input.GetKeyDown(KeyCode.R) && isGameDone)
            SceneManager.LoadScene(currentSceneName);
    }

    private void EndGame()
    {
        hud.ToggleHud();
        endGame.ActiveDeathScene();
        isGameDone=true;
    }
}
