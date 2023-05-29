using FrontEnd;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//[RequireComponent(typeof(ILeaderboard))]
public class LeaderboardView : MenuPanel
{
    [Header("Leaderboard Panel")]
    [SerializeField] private TextMeshProUGUI[] indexes;
    [SerializeField] private TextMeshProUGUI[] names;
    [SerializeField] private TextMeshProUGUI[] scores;
    [SerializeField] private TextMeshProUGUI[] units;

    private ILeaderboard leaderboard;
    private int actualPageNumber = 1;
    private int recordsOnPage = 13;

    private void Awake()
    {
        leaderboard = new OfflineLeaderboard();
    }

    protected override void Enable()
    {
        actualPageNumber = 1;
        UpdateScoreboard();
    }

    public void UpdateScoreboard()
    {
        var scoreboard = new List<LeaderboardRecord>(leaderboard.RequestRecords());

        for (int i = 0; i < indexes.Length; i++)
        {
            indexes[i].text = (i + 1 + recordsOnPage * (actualPageNumber - 1)).ToString();

            names[i].text = (i + recordsOnPage * (actualPageNumber - 1) < scoreboard.Count)
                ? scoreboard[i + recordsOnPage * (actualPageNumber - 1)].name
                : string.Empty;

            scores[i].text = (i + recordsOnPage * (actualPageNumber - 1) < scoreboard.Count)
                ? scoreboard[i + recordsOnPage * (actualPageNumber - 1)].value.ToString()
                : string.Empty;

            units[i].text = (i + recordsOnPage * (actualPageNumber - 1) < scoreboard.Count)
                ? "m"
                : string.Empty;
        }
    }

    public void AddNewRecord(LeaderboardRecord record)
    {
        leaderboard.PushNewScore(record);
    }

    #region BUTTONS
    public void PreviousPageButton()
    {
        if (actualPageNumber > 1)
            actualPageNumber--;
        UpdateScoreboard();
    }
    public void NextPageButton()
    {
        actualPageNumber++;
        UpdateScoreboard();
    }
    public void RefreshTable()
    {
        UpdateScoreboard();
    }
    #endregion

    [ContextMenu("Add New Record")]
    public void AddNewRecord()
    {
        var randNames = new List<string>()
        {
            "Suvrovrud",
            "Dethadar Veder",
            "Glaar Khertam",
            "Naspa Morin",
            "Delazoc",
            "Hinturrmaillin",
            "Tazo Imvume",
            "Kolbilg",
            "Adraazac",
            "Errante Kaelock",
            "Baludon",
            "Velryn Boalrith",
            "Merden Sethel",
        };

        leaderboard.PushNewScore(new LeaderboardRecord()
        {
            name = randNames[Random.Range(0, randNames.Count)],
            value = Random.Range(0, 4999),
        });
        UpdateScoreboard();
    }
}

[System.Serializable]
public struct LeaderboardRecord
{
    public string name;
    public int value;
}