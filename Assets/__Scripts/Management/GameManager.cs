using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStates
{
    RUNNING,
    PAUSED
}

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [Header("GameSettings")]
    [SerializeField] private GameStates gameState;
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private int currentStage = 1;

    [Header("Pooling System Reference's")]
    [Tooltip("Create one empty Object and configure the Pooling System on there and after this " +
             "reference here the object ### IMPORTANT ### be careful and create one per each type")]
    public PoolingSystem bulletPoolSystem;
    [Tooltip("Create one empty Object and configure the Pooling System on there and after this " +
             "reference here the object ### IMPORTANT ### be careful and create one per each type")]
    public PoolingSystem asteroidCompositionPoolSystem;
    [Tooltip("Create one empty Object and configure the Pooling System on there and after this " +
             "reference here the object ### IMPORTANT ### be careful and create one per each type")]
    public PoolingSystem singleAsteroidPoolSystem;
    [Tooltip("Create one empty Object and configure the Pooling System on there and after this " +
             "reference here the object ### IMPORTANT ### be careful and create one per each type")]
    public PoolingSystem explosionVfxPoolSystem;
    [Tooltip("Create one empty Object and configure the Pooling System on there and after this " +
             "reference here the object ### IMPORTANT ### be careful and create one per each type")]
    public PoolingSystem playerHitVfxPoolSystem;

    [Header("Game Initial Stats")] 
    [SerializeField] private int initialLife = 3;
    
    [Header("HUD Reference's")] 
    [SerializeField] private TextMeshProUGUI scoreTMP;
    [SerializeField] private TextMeshProUGUI lifeTMP;
    [SerializeField] private TextMeshProUGUI timePlayingTMP;

    [Header("Screen Reference's")] 
    [SerializeField] private ScreenBase gameScreen;
    [SerializeField] private ScreenBase pauseScreen;
    [SerializeField] private ScreenBase levelPassedScreen;
    [SerializeField] private ScreenBase gameOverScreen;
    

    private int _life = 0;
    private int _score = 0;
    private float _timePlaying = 0;
    
    //In Game Token, Cancel when game is not running
    private static CancellationToken _cancelationToken;
    private static CancellationTokenSource source;


    public static CancellationToken InGameCancellationToken => _cancelationToken;

    public int CurrentStage
    {
        get { return currentStage; }
        set { currentStage = value; }
    }

    public GameSettings GameSettings => gameSettings;
    
    public int LifeRemaining
    {
        get { return _life; }
        set
        {
            _life = value;
            lifeTMP.text = $"Life: {_life.ToString("00")}";
        }
    }
    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            scoreTMP.text = $"Score: {_score}";
            if(_score >= currentStage * 100)
                NextLevelCall();
        }
    }
    public float TimePlaying
    {
        get { return _timePlaying; }
        set
        {
            _timePlaying = value;
            timePlayingTMP.text = $"Time: {(int)_timePlaying}";
        }
    }
    
    public GameStates GameState
    {
        get { return gameState; }
        set { gameState = value; }
    }

    public void Initialize()
    {
        gameState = GameStates.RUNNING;
        gameScreen.ShowScreen();
        Time.timeScale = 1;
    }
    
    private void Awake()
    {
        ResetStats();
    }

    private void Update()
    {
        if (gameState == GameStates.RUNNING)
            TimePlaying += Time.deltaTime;
    }

    private void SetupTaskCancelationControll()
    {
        source = new CancellationTokenSource();
        _cancelationToken = source.Token;
    }

    public void CallGameOver()
    {
        gameState = GameStates.PAUSED;
        gameOverScreen.ShowScreen();
    }

    public void Pause()
    {
        if (gameState == GameStates.PAUSED) return;
        gameState = GameStates.PAUSED;
        gameScreen.CloseScreen(null);
        pauseScreen.ShowScreen();
        Time.timeScale = 0;
    }

    private void NextLevelCall()
    {
        gameState = GameStates.PAUSED;
        levelPassedScreen.ShowScreen();
    }
    
    public void Unpause()
    {
        pauseScreen.CloseScreen(Initialize);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private void ResetStats()
    {
        LifeRemaining = initialLife;
        Score = 0;
        TimePlaying = 0;
    }

    private void OnEnable()
    {
        if (source != null && source.IsCancellationRequested)
            SetupTaskCancelationControll();
    }

    private void OnDisable()
    {
        source?.Cancel();
        source?.Dispose();
    }

    private void OnApplicationQuit()
    {
        source?.Cancel();
        source?.Dispose();
    }

    private void OnDestroy()
    {
        source?.Cancel();
    }
    
}
