using Cinemachine;
using System.Threading.Tasks;
using UnityEngine;

public class AdventureModeManager : LevelManager
{
    [Header("Adventure Mode Component")]
    [SerializeField] protected int levelNumber;
    [Space]
    [Header("Virtual Camera Components")]
    [SerializeField] protected CinemachineVirtualCamera startCamera;
    [SerializeField] protected CinemachineVirtualCamera gameCamera;
    [SerializeField] protected CinemachineVirtualCamera finishCamera;

    [Header("Level Properties")]
    public FloatVariable startCoinsValue;
    public FloatVariable finishCoinsValue;

    private const int FINISH_PANEL_DELAY_TIME_MILLIS = 2000;
    private const int SECOND_STAR_REQUIREMENT = 50;
    private const int THIRD_STAR_REQUIREMENT = 100;

    protected override void GameSetup()
    {
        base.GameSetup();

        startCoinsValue.value = Coin.Coins.Count;
        finishCoinsValue.value = 0;

        startCamera.m_Priority = 10;
        gameCamera.m_Priority = 0;
        finishCamera.m_Priority = 0;

        flock.transform.position = Vector3.zero;
    }

    protected override void StartGame()
    {
        base.StartGame();

        startCamera.m_Priority = 0;
        gameCamera.m_Priority = 10;
        finishCamera.m_Priority = 0;
    }

    protected override void FinishGame()
    {
        if (Agent.ActiveAgents.Count <= 0)
        {
            LoseCondition();
        }

        else if (finish.Finished)
        {
            WinCondition();
        }
    }

    private async void WinCondition()
    {
        var finalScore = finishCoinsValue.value * 100 / startCoinsValue.value;
        Debug.Log($"[Level Manager]Final score: {finishCoinsValue.value}/{startCoinsValue.value} ({Mathf.FloorToInt(finalScore)}%)");

        // Virtual cameras movement
        startCamera.m_Priority = 0;
        gameCamera.m_Priority = 0;
        finishCamera.m_Priority = 10;

        // Update and save data
        UpdatePersistenceData(CalculateStars(finalScore), levelNumber);

        //if (DataPersistenceManager.Instance)
        //    DataPersistenceManager.Instance.SaveGame();

        // Open finish panel with delay
        await Task.Delay(FINISH_PANEL_DELAY_TIME_MILLIS);
        //finishGame?.Invoke(true, CalculateStars(finalScore));
    }

    private void LoseCondition()
    {
        // Stop agents
        flock.SetSpeed(0.0f);

        // Update and save data
        //if (DataPersistenceManager.Instance)
        //    DataPersistenceManager.Instance.SaveGame();

        // Open finish panel with delay
        //await Task.Delay(FINISH_DELAY_TIME_MILLIS);
    }

    private int CalculateStars(float finalScore)
    {
        if (finalScore < SECOND_STAR_REQUIREMENT)
            return 1;

        else if (finalScore >= SECOND_STAR_REQUIREMENT && finalScore != THIRD_STAR_REQUIREMENT)
            return 2;

        else if (finalScore == THIRD_STAR_REQUIREMENT)
            return 3;

        else
            return 0;
    }

    private void UpdatePersistenceData(int stars, int level)
    {
        /*var actualLevel = dataStore.levelsData.Find(l => l.levelNumber == level);
        var nextLevel = dataStore.levelsData.Find(l => l.levelNumber == level + 1);

        if (actualLevel.stars < stars)
            actualLevel.stars = stars;

        nextLevel.available = true;*/
    }
}