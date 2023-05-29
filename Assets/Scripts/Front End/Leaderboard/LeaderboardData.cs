using System;
using System.Collections.Generic;

[Serializable]
public class LeaderboardData
{
    public List<LeaderboardRecord> leaderboard;

    public LeaderboardData()
    {
        leaderboard = new List<LeaderboardRecord>();
    }
}