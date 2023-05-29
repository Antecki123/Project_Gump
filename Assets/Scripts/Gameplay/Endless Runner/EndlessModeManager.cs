using Cinemachine;
using System.Threading.Tasks;
using UnityEngine;

public class EndlessModeManager : LevelManager
{
    [Header("Virtual Camera Components")]
    [SerializeField] protected CinemachineVirtualCamera startCamera;
    [SerializeField] protected CinemachineVirtualCamera gameCamera;

    private const int FINISH_PANEL_DELAY_TIME_MILLIS = 1000;

    protected override void GameSetup()
    {
        base.GameSetup();

        startCamera.m_Priority = 10;
        gameCamera.m_Priority = 0;
    }

    protected override void StartGame()
    {
        base.StartGame();

        startCamera.m_Priority = 0;
        gameCamera.m_Priority = 10;
    }

    protected async override void FinishGame()
    {
        // Stop agents
        flock.SetSpeed(0.0f);

        // Open finish panel with delay
        await Task.Delay(FINISH_PANEL_DELAY_TIME_MILLIS);
    }
}