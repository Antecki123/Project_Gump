using System;

[Serializable]
public class GameSettingsData
{
    public float settingsMusic;
    public float settingsEffects;

    public GraphicQuality settingsQuality;
    public Language settingsLanguage;

    public bool settingsVibrations;

    public string playerName;

    public GameSettingsData()
    {
        settingsMusic = 0.5f;
        settingsEffects = 0.5f;

        settingsQuality = GraphicQuality.High;
        settingsLanguage = Language.English;

        settingsVibrations = true;

        playerName = "";
    }
}