using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    [Header("Gameplay")]
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private CharacterFactory characterFactory;
    [SerializeField] private GameData gameData;

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

    public GameData ChosenLevel { get; private set; }
    private bool isGameActive = false;
    private bool isGamePaused = false;
    private float gameTimeSeconds = 0;

    public float GameTimeSeconds => gameTimeSeconds;
    public bool IsGameActive => isGameActive;

    private float gameSessionTime;
    private float timeBetweenEnemySpawn;
    private int entityNumberAtTime;
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
        //InputService = new NewInputService();
        windowsService.Initialize();
        controlSettingManager.Initialize();
        soundManager.Initialize();
        levelManager.Initialize();
        isGameActive = false;
    }

    public void StartGame()
    {
        if (isGameActive) return;

        ScoreSystem.CurrentLevel = levelManager.selectedLevel;
        characterFactory.DeleteAllCharacters();
        windowsService.HideAllWindow(false);

        Character player = characterFactory.GetCharacter(CharacterType.Player);
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
        player.Initialize();
        player.LiveComponent.OnCharacterDeath += CharacterDeathHandler;

        gameTimeSeconds = 0;
        gameSessionTime = 0;
        timeBetweenEnemySpawn = gameData.TimeBetweenEnemySpawn;
        entityNumberAtTime = 1;
        maxEntityNumberAtTime = gameData.MaxEntityNumberAtTime;

        ScoreSystem.StartGame();
        audioService.ToggleAmbientSound(AmbientType.MainMenuAmbient, ambientSource, true);
        isGameActive = true;

    }

    public void StopGame()
    {
        audioService.ToggleAmbientSound(AmbientType.MainMenuAmbient, ambientSource, false);
        characterFactory.DeleteAllCharacters();
        effectsFactory.RemoveAll();
        windowsService.HideAllWindow(false);
        ScoreSystem.EndGameLoose();
        isGameActive = false;
    }

    public void LeaveGame()
    {
        audioService.ToggleAmbientSound(AmbientType.MainMenuAmbient, ambientSource, false);
        characterFactory.DeleteAllCharacters();
        effectsFactory.RemoveAll();
        windowsService.HideAllWindow(false);
        isGameActive = false;
    }

    public void PauseGame()
    {
        if (isGamePaused)
        {
            audioService.ToggleAmbientSound(AmbientType.MainMenuAmbient, ambientSource, true);
            Time.timeScale = 1.0f;
            isGamePaused = false;
            windowsService.HideWindow<PauseWindow>(false);
            return;
        }
        audioService.ToggleAmbientSound(AmbientType.MainMenuAmbient, ambientSource, false);
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

        maxEntityNumberAtTime = gameData.MaxEntityNumberAtTime + Mathf.FloorToInt(gameSessionTime / 5);

        if (timeBetweenEnemySpawn < 0 && entityNumberAtTime < maxEntityNumberAtTime)
        {
            SpawnEnemy();
            timeBetweenEnemySpawn = gameData.TimeBetweenEnemySpawn;
        }

        if (gameTimeSeconds > gameData.SessionTimeSeconds)
        {
            GameVictory();
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
        entityNumberAtTime++;

        float GetOffset()
        {
            bool isPlus = Random.Range(0, 100) % 2 != 0;
            float offset = Random.Range(gameData.MinEnemySpawnOffset, gameData.MaxEnemySpawnOffset);
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
            ScoreSystem.AddXP(deadCharacter.CharacterData.XPCost);
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
}
