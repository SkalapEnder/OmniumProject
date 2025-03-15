using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData gameData; 
    [SerializeField] private CharacterFactory characterFactory;
    private ScoreSystem scoreSystem;

    private float gameSessionTime;
    private float timeBetweenEnemySpawn;
    private int entityNumberAtTime;
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
        else {
            Destroy(this.gameObject);
        }
    }

    private void Initialize()
    {
       scoreSystem = new ScoreSystem();
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
        entityNumberAtTime = 1;
        timeBetweenEnemySpawn = gameData.TimeBetweenEnemySpawn;
        
        scoreSystem.StartGame();
        isGameActive = true;
    }

    private void Update()
    {
        if(!isGameActive) return;

        gameSessionTime += Time.deltaTime;
        timeBetweenEnemySpawn -= Time.deltaTime;

        if (timeBetweenEnemySpawn < 0)
        {
            if (entityNumberAtTime < gameData.MaxEntityNumberAtTime)
            {
                SpawnEnemy();
            }
            timeBetweenEnemySpawn = gameData.TimeBetweenEnemySpawn;
        }

        if(gameSessionTime > gameData.SessionTimeSeconds)
        {
            GameVictory();
        }
    }

    private void CharacterDeathHandler(Character deadCharacter)
    {
        switch (deadCharacter.CharacterType)
        {
            case CharacterType.Player:
                GameOver();
                break;

            case CharacterType.EnemyDefault:
                scoreSystem.AddScore(deadCharacter.CharacterData.ScoreCost);
                break;
        }

        deadCharacter.gameObject.SetActive(false);
        characterFactory.ReturnCharacter(deadCharacter);
        deadCharacter.LiveComponent.OnCharacterDeath -= CharacterDeathHandler;
        entityNumberAtTime -= 1;
    }

    private void SpawnEnemy()
    {
        Character enemy = CharacterFactory.GetCharacter(CharacterType.EnemyDefault);
        Vector3 playerPosition = characterFactory.Player.transform.position;
        enemy.transform.position = new Vector3(playerPosition.x + GetOffset(), 0, playerPosition.z + GetOffset());
        enemy.gameObject.SetActive(true);
        enemy.Initialize();
        enemy.LiveComponent.OnCharacterDeath += CharacterDeathHandler;
        entityNumberAtTime += 1;

        float GetOffset()
        {
            bool isPlus = Random.Range(0, 100) % 2 != 0;
            float offset = Random.Range(gameData.MinEnemySpawnOffset, gameData.MaxEnemySpawnOffset);
            return (isPlus) ? offset : (-1 * offset);
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
