using System.Collections.Generic;
using UnityEngine;

public class OfflineLeaderboard : ILeaderboard
{
    [Header("File Storage Config")]
    private string fileName = "score.txt";
    private bool useEncryption = false;

    private LeaderboardData leaderboardData;
    private FileDataHandler<LeaderboardData> dataHandler;

    public OfflineLeaderboard()
    {
        dataHandler = new FileDataHandler<LeaderboardData>(Application.persistentDataPath, fileName, useEncryption);
    }

    public List<LeaderboardRecord> RequestRecords()
    {
        var records = new List<LeaderboardRecord>();

        leaderboardData = dataHandler.Load();

        if (leaderboardData == null)
        {
            Debug.Log($"[Leaderboard] No files to load!");

            leaderboardData = new LeaderboardData();
            dataHandler.Save(leaderboardData);
        }
        else
        {
            records.AddRange(leaderboardData.leaderboard);
        }

        return records;
    }

    public void PushNewScore(LeaderboardRecord record)
    {
        var records = new List<LeaderboardRecord>(RequestRecords())
        {
            record
        };

        records.Sort((x, y) => y.value.CompareTo(x.value));

        if (leaderboardData == null)
        {
            Debug.Log($"[Leaderboard] No files to save!");

            leaderboardData = new LeaderboardData();
            dataHandler.Save(leaderboardData);
        }
        else
        {
            leaderboardData.leaderboard.Clear();
            leaderboardData.leaderboard.AddRange(records);
        }

        dataHandler.Save(leaderboardData);
    }
}