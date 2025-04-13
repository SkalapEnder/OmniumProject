using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    [Header("Gameplay")]
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private SkillService skillService;
    [SerializeField] private CharacterFactory characterFactory;
    [SerializeField] private GameData gameData;
    [SerializeField] private Transform upperLeftEnd;
    [SerializeField] private Transform lowerRightEnd;

    [Space(10), Header("UI & Settings")]
    [SerializeField] private ControlSettingManager controlSettingManager;
    [SerializeField] private SimpleAudioSystemService audioService;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private WindowService windowsService;

    [Space(10), Header("Audio & Effects")]
    [SerializeField] private AudioSystemService audioSystemService;
    [SerializeField] private AudioSource ambientSource;
    [SerializeField] private EffectsFactory effectsFactory;

    public static GameManager Instance { get; private set; }
    public ScoreSystem ScoreSystem { get; private set; }

    public CharacterFactory CharacterFactory =>
        characterFactory;

    public WindowService WindowsService =>
        windowsService;

    public ControlSettingManager ControlSettingManager =>
        controlSettingManager;

    public EffectsFactory EffectsFactory =>
        effectsFactory;


    //public IInputService InputService { get; private set; }

    public SoundManager SoundManager => soundManager;
    public SimpleAudioSystemService AudioService => audioService;
    public LevelManager LevelManager => levelManager;

    public bool IsHealingEnabled = false;
    public bool IsXPBoostEnabled = false;
    public bool IsDamageBoostEnabled = false;
    public bool IsAddHPEnabled = false;

    public GameData ChosenLevel { get; private set; }
    public int MaxItemNumber { get; set; } = 5;
    private bool isGameActive = false;
    private bool isGamePaused = false;
    private float gameTimeSeconds = 0;

    public float GameTimeSeconds => gameTimeSeconds;
    public bool IsGameActive => isGameActive;

    private float gameSessionTime;
    private float timeBetweenItemSpawn;
    private float timeBetweenEnemySpawn;

    private int entityNumberAtTime;
    private int itemNumberAtTime;
    private int maxEntityNumberAtTime;

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
        ScoreSystem = new ScoreSystem();
        windowsService.Initialize();
        controlSettingManager.Initialize();
        soundManager.Initialize();

        skillService.Initialize();
        levelManager.Initialize();

        isGameActive = false;
    }

    public void StartGame()
    {
        if (isGameActive) return;

        ScoreSystem.CurrentLevel = levelManager.selectedLevel;
        characterFactory.DeleteAllCharacters();
        windowsService.HideAllWindow(false);
        ChosenLevel = levelManager.selectedLevel.LevelData;

        Character player = characterFactory.GetCharacter(CharacterType.Player);
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        player.Initialize();
        player.LiveComponent.OnCharacterDeath += CharacterDeathHandler;

        gameTimeSeconds = 0;
        gameSessionTime = 0;
        timeBetweenEnemySpawn = ChosenLevel.TimeBetweenEnemySpawn;
        timeBetweenItemSpawn = ChosenLevel.TimeBetweenItemSpawn;
        entityNumberAtTime = 1;
        itemNumberAtTime = 0;
        maxEntityNumberAtTime = ChosenLevel.MaxEntityNumberAtTime;

        ScoreSystem.StartGame();
        audioService.ToggleAmbientSound(AmbientType.MainMenuAmbient, ambientSource, true);
        isGameActive = true;

    }

    public void StopGame()
    {
        characterFactory.DeleteAllCharacters();
        effectsFactory.RemoveAll();

        audioService.ToggleAmbientSound(AmbientType.MainMenuAmbient, ambientSource, false);
        windowsService.HideAllWindow(false);

        ScoreSystem.EndGameLoose();
        isGameActive = false;
    }

    public void LeaveGame()
    {
        characterFactory.DeleteAllCharacters();
        effectsFactory.RemoveAll();

        audioService.ToggleAmbientSound(AmbientType.MainMenuAmbient, ambientSource, false);
        windowsService.HideAllWindow(false);
        isGameActive = false;
    }

    public void PauseGame()
    {
        if (isGamePaused)
        {
            audioService.SetVolume(AudioSystemType.Ambient, false);
            Time.timeScale = 1.0f;
            isGamePaused = false;
            windowsService.HideWindow<PauseWindow>(false);
            return;
        }

        audioService.SetVolume(AudioSystemType.Ambient, true);
        Time.timeScale = 0.0f;
        windowsService.ShowWindow<PauseWindow>(false);
        isGamePaused = true;
    }

    private void Update()
    {
        if (!isGameActive)
            return;

        gameTimeSeconds += Time.deltaTime;
        gameSessionTime += Time.deltaTime;
        timeBetweenEnemySpawn -= Time.deltaTime;
        timeBetweenItemSpawn -= Time.deltaTime;

        maxEntityNumberAtTime = ChosenLevel.MaxEntityNumberAtTime + Mathf.FloorToInt(gameSessionTime / 5);

        if(timeBetweenItemSpawn < 0 && itemNumberAtTime < MaxItemNumber)
        {
            // Item spawn;
        }

        if (timeBetweenEnemySpawn < 0 && entityNumberAtTime < maxEntityNumberAtTime)
        {
            SpawnEnemy();
            timeBetweenEnemySpawn = ChosenLevel.TimeBetweenEnemySpawn;
        }

        if (gameTimeSeconds > ChosenLevel.SessionTimeSeconds)
        {
            GameVictory();
        }
    }

    private void SpawnEnemy()
    {
        Character enemy = characterFactory.GetCharacter(CharacterType.EnemyDefault);
        Vector3 playerPosition = characterFactory.PlayerCharacter.transform.position;

        bool isPositionCorrect = false;
        Vector3 enemyPosition = Vector3.zero;

        while (!isPositionCorrect)
        {
            enemyPosition = new Vector3(playerPosition.x + GetOffset(), 0, playerPosition.z + GetOffset());
            if((enemyPosition.x < upperLeftEnd.position.x || enemyPosition.z > upperLeftEnd.position.z) || 
                (enemyPosition.x > lowerRightEnd.position.x || enemyPosition.z < lowerRightEnd.position.z))
            {
                return;
            }

            isPositionCorrect = true;
        }
       
        enemy.transform.position = enemyPosition;
        enemy.gameObject.SetActive(true);
        enemy.Initialize();
        enemy.LiveComponent.OnCharacterDeath += CharacterDeathHandler;
        entityNumberAtTime++;

        float GetOffset()
        {
            bool isPlus = Random.Range(0, 100) % 2 != 0;
            float offset = Random.Range(ChosenLevel.MinEnemySpawnOffset, ChosenLevel.MaxEnemySpawnOffset);
            return ((isPlus) ? offset : (-1 * offset));
        }
    }

    private void CharacterDeathHandler(Character deadCharacter)
    {
        deadCharacter.gameObject.SetActive(false);
        characterFactory.ReturnCharacter(deadCharacter);
        deadCharacter.LiveComponent.OnCharacterDeath -= CharacterDeathHandler;
        entityNumberAtTime--;

        if (deadCharacter.CharacterType == CharacterType.Player)
        {
            GameOver();
        }
        else if (deadCharacter.CharacterType == CharacterType.EnemyDefault)
        {
            Debug.Log("Enemy Defeated");
            ScoreSystem.AddScore(deadCharacter.CharacterData.ScoreCost);
            ScoreSystem.AddXP((int)(deadCharacter.CharacterData.XPCost * (IsXPBoostEnabled ? 1.2f : 1f)));
        }
    }

    private void GameVictory()
    {
        ScoreSystem.EndGameWin();
        windowsService.HideWindow<GameplayWindow>(true);
        windowsService.ShowWindow<VictoryWindow>(false);
        levelManager.Restart();
        isGameActive = false;
    }

    private void GameOver()
    {
        ScoreSystem.EndGameLoose();
        windowsService.HideWindow<GameplayWindow>(true);
        windowsService.ShowWindow<LooseWindow>(false);
        isGameActive = false;
    }

    public void ClearProgress()
    {
        ScoreSystem.ClearProgress();
        skillService.CancelSkills();
        levelManager.ResetProgress();
    }
}
