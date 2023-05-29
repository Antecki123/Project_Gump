using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;

public class LeaderboardUnityServices : ILeaderboard
{
    private List<LeaderboardRecord> records = new List<LeaderboardRecord>();

    private string playerID = string.Empty;
    private readonly string leaderboardId = "9938ddd8-c572-4dc7-aa15-35af8b95e1f9";

    public LeaderboardUnityServices(string playerID)
    {
        this.playerID = playerID;

        ScoreboardInitialize();
    }

    public List<LeaderboardRecord> RequestRecords()
    {
        return records;
    }

    public async void PushNewScore(LeaderboardRecord record)
    {
        await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardId, record.value);
    }

    private async void ScoreboardInitialize()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
}