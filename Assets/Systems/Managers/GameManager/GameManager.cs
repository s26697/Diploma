using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState State { get; private set; } = GameState.None;

    [SerializeField] private WaveManager waveManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnEnable()
    {
        GameEvents.OnWaveCompleted += HandleWaveCompleted;
        GameEvents.OnPlayerDied += HandlePlayerDied;
    }

    private void OnDisable()
    {
        GameEvents.OnWaveCompleted -= HandleWaveCompleted;
        GameEvents.OnPlayerDied -= HandlePlayerDied;
    }

    // Game flow f

    public void StartGame()
    {
        ChangeState(GameState.Playing);
        GameEvents.GameStarted();
    }

    public void PauseGame()
    {
        if (State != GameState.Playing) return;

        ChangeState(GameState.Paused);
        GameEvents.GamePaused();
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        if (State != GameState.Paused) return;

        ChangeState(GameState.Playing);
        GameEvents.GameResumed();
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        ChangeState(GameState.None);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        //TODO
    }


    private void ChangeState(GameState newState)
    {
        State = newState;
    }

    private void HandleWaveCompleted(int waveIndex)
    {
        // TODO handling wygrania
        if (waveIndex == waveManager.TotalWaves - 1)
        {
            ChangeState(GameState.GameOver);
            GameEvents.GameWon();
        }
    }

    private void HandlePlayerDied()
    {
        ChangeState(GameState.GameOver);
        GameEvents.GameLost();
    }
}
