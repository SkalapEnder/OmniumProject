using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawnController : MonoBehaviour
{
    private CharacterFactory characterFactory;
    private GameData gameData;

    private float gameSessionTime;
    private float timeBetweenEnemySpawn;
    private int entityNumberAtTime;
    private int maxEntityNumberAtTime;

    public void Initialize(CharacterFactory factory, GameData data)
    {
        characterFactory = factory;
        gameData = data;

        gameSessionTime = 0;
        timeBetweenEnemySpawn = gameData.TimeBetweenEnemySpawn;
        entityNumberAtTime = 1;
        maxEntityNumberAtTime = gameData.MaxEntityNumberAtTime;
    }

    public void UpdateSpawn(float deltaTime)
    {
        gameSessionTime += deltaTime;
        timeBetweenEnemySpawn -= deltaTime;

        // Увеличение максимального количества врагов каждую минуту
        maxEntityNumberAtTime = gameData.MaxEntityNumberAtTime + Mathf.FloorToInt(gameSessionTime / 2);
        //Debug.Log(maxEntityNumberAtTime);
        if (timeBetweenEnemySpawn < 0)
        {
            if (entityNumberAtTime < maxEntityNumberAtTime)
            {
                SpawnEnemy();
            }
            timeBetweenEnemySpawn = gameData.TimeBetweenEnemySpawn;
        }
    }

    private void SpawnEnemy()
    {
        Character enemy = characterFactory.GetCharacter(CharacterType.EnemyDefault);
        Vector3 playerPosition = characterFactory.PlayerCharacter.transform.position;
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

    private void CharacterDeathHandler(Character deadCharacter)
    {
        deadCharacter.gameObject.SetActive(false);
        characterFactory.ReturnCharacter(deadCharacter);
        deadCharacter.LiveComponent.OnCharacterDeath -= CharacterDeathHandler;
        entityNumberAtTime -= 1;
    }
}

