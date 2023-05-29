using System.Collections.Generic;

public interface ILeaderboard
{
    public List<LeaderboardRecord> RequestRecords();
    public void PushNewScore(LeaderboardRecord record);
}