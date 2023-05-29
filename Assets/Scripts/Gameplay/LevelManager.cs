using System;
using System.Threading.Tasks;
using UnityEngine;

[DefaultExecutionOrder(40)]
public abstract class LevelManager : MonoBehaviour
{
    public static Action setupGame;
    public static Action startGame;
    public static Action finishGame;
    public static Action pauseGame;

    public static GameState GameState { get; private set; } = GameState.FrontEnd;

    [Header("Component References")]
    protected FlockController flock;
    protected Finish finish;

    private Transform flockTransform;

    [Header("Game Stats")]
    [SerializeField] private AgentsStorage agentsStorage;
    [Space]
    [SerializeField] protected FloatVariable agentsAmount;
    [SerializeField] protected FloatVariable score;
    [SerializeField] protected StringVariable player;

    [Header("Audio")]
    [SerializeField] private AudioSoundtrack soundtrack;

    private void Awake()
    {
        flock = FindFirstObjectByType<FlockController>();
        finish = FindFirstObjectByType<Finish>();

        flockTransform = flock.transform;
    }

    private void OnEnable()
    {
        setupGame += GameSetup;
        startGame += StartGame;
        finishGame += FinishGame;
        pauseGame += PauseGame;

        AgentsFactory.OnRemoveAgent += CheckEndGameConditions;
    }
    private void OnDisable()
    {
        setupGame -= GameSetup;
        startGame -= StartGame;
        finishGame -= FinishGame;
        pauseGame -= PauseGame;

        AgentsFactory.OnRemoveAgent -= CheckEndGameConditions;
    }

    private void Start() => setupGame?.Invoke();

    private void Update()
    {
        score.value = flockTransform.position.z;
        agentsAmount.value = Agent.ActiveAgents.Count;
    }

    protected virtual void GameSetup()
    {
        score.value = 0.0f;
        agentsAmount.value = 0.0f;

        flock.SetSpeed(0.0f);
    }

    protected virtual void StartGame()
    {
        if (AudioManager.Instance)
            AudioManager.Instance.Play(soundtrack);

        flock.SetSpeed(agentsStorage.activeAgent.agentData.velocity);

        GameState = GameState.GameInProgress;
    }

    protected virtual void PauseGame()
    {
        GameState = GameState != GameState.GamePaused
            ? GameState.GamePaused
            : GameState.GameInProgress;

        if (GameState == GameState.GamePaused)
            Time.timeScale = 0.0f;
        else
            Time.timeScale = 1.0f;
    }

    protected virtual void FinishGame() { }

    private async void CheckEndGameConditions(Agent _)
    {
        await Task.Yield();

        if (GameState == GameState.GameInProgress && Agent.ActiveAgents.Count <= 0)
        {
            GameState = GameState.FrontEnd;
            finishGame?.Invoke();
        }
    }

#if UNITY_EDITOR
    public void StartGameButton() => StartGame();
    public void ResetGameButton() => setupGame?.Invoke();
    public void PauseGameButton() => pauseGame?.Invoke();
#endif
}

public enum GameState
{
    FrontEnd,
    GameInProgress,
    GamePaused,
}