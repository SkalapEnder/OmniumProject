using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private CharacterFactory characterFactory;
    private ScoreSystem scoreSystem;
    private CharacterSpawnController spawnController;

    private float gameSessionTime;
    private bool isGameActive;

    public static GameManager Instance { get; private set; }
    public CharacterFactory CharacterFactory => characterFactory;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Initialize();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Initialize()
    {
        scoreSystem = new ScoreSystem();
        spawnController = new CharacterSpawnController();
        spawnController.Initialize(characterFactory, gameData);
        isGameActive = false;
    }

    public void StartGame()
    {
        if (isGameActive)
            return;

        Character player = CharacterFactory.GetCharacter(CharacterType.Player);
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        player.Initialize();
        player.LiveComponent.OnCharacterDeath += CharacterDeathHandler;

        gameSessionTime = 0;

        spawnController.Initialize(characterFactory, gameData);
        scoreSystem.StartGame();
        isGameActive = true;
    }

    private void Update()
    {
        if (!isGameActive) return;

        spawnController.UpdateSpawn(Time.deltaTime);
        gameSessionTime += Time.deltaTime;

        if (gameSessionTime > gameData.SessionTimeSeconds)
        {
            GameVictory();
        }
    }

    private void CharacterDeathHandler(Character deadCharacter)
    {
        if (deadCharacter.CharacterType == CharacterType.Player)
        {
            GameOver();
        }
        else if (deadCharacter.CharacterType == CharacterType.EnemyDefault)
        {
            scoreSystem.AddScore(deadCharacter.CharacterData.ScoreCost);
        }
    }

    private void GameVictory()
    {
        scoreSystem.EndGame();
        Debug.Log("You win!");
        isGameActive = false;
    }

    private void GameOver()
    {
        scoreSystem.EndGame();
        Debug.Log("You dead! Not big surprise");
        isGameActive = false;
    }
}
