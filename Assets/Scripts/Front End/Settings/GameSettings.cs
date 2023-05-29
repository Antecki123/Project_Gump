using System;
using UnityEngine;

public class GameSettings
{
    [Header("File Storage Config")]
    private string fileName = "settings.ini";
    private bool useEncryption = false;

    private GameSettingsScriptable settingsDataScriptable;
    private GameSettingsData settingsData;
    private FileDataHandler<GameSettingsData> dataHandler;

    private LocalizationManager localizationManager;

    public GameSettings(GameSettingsScriptable settingsDataScriptable)
    {
        this.settingsDataScriptable = settingsDataScriptable;
        dataHandler = new FileDataHandler<GameSettingsData>(Application.persistentDataPath, fileName, useEncryption);
        LoadSettings();

        QualitySettings.SetQualityLevel((int)settingsDataScriptable.quality);
        localizationManager = new LocalizationManager(settingsDataScriptable.language);
    }

    #region SETTINGS PERSISTENCE
    public void LoadSettings()
    {
        settingsData = dataHandler.Load();

        CreateNewSettings();

        settingsDataScriptable.music = settingsData.settingsMusic;
        settingsDataScriptable.effects = settingsData.settingsEffects;
        settingsDataScriptable.quality = settingsData.settingsQuality;
        settingsDataScriptable.language = settingsData.settingsLanguage;
        settingsDataScriptable.vibrations = settingsData.settingsVibrations;
        settingsDataScriptable.playerName.text = settingsData.playerName;
    }

    public void SaveSettings()
    {
        CreateNewSettings();

        settingsData.settingsMusic = settingsDataScriptable.music;
        settingsData.settingsEffects = settingsDataScriptable.effects;
        settingsData.settingsQuality = settingsDataScriptable.quality;
        settingsData.settingsLanguage = settingsDataScriptable.language;
        settingsData.settingsVibrations = settingsDataScriptable.vibrations;
        settingsData.playerName = settingsDataScriptable.playerName.text;

        dataHandler.Save(settingsData);
    }

    private void CreateNewSettings()
    {
        if (settingsData == null)
        {
            Debug.Log($"[Settings] No files found!");

            settingsData = new GameSettingsData();
            dataHandler.Save(settingsData);
        }
    }
    #endregion

    #region SETTINGS LOGIC
    public void DecreaseLanguage()
    {
        if ((int)settingsDataScriptable.language > 0)
            settingsDataScriptable.language--;

        localizationManager.SetLocale(settingsDataScriptable.language);
    }

    public void IncreaseLanguage()
    {
        var enumMemberCount = Enum.GetNames(typeof(Language)).Length;

        if ((int)settingsDataScriptable.language < enumMemberCount - 1)
            settingsDataScriptable.language++;

        localizationManager.SetLocale(settingsDataScriptable.language);
    }

    public void DecreaseGraphicQuality()
    {
        if ((int)settingsDataScriptable.quality > 0)
            settingsDataScriptable.quality--;

        QualitySettings.SetQualityLevel((int)settingsDataScriptable.quality);
    }

    public void IncreaseGraphicQuality()
    {
        var enumMemberCount = Enum.GetNames(typeof(GraphicQuality)).Length;

        if ((int)settingsDataScriptable.quality < enumMemberCount - 1)
            settingsDataScriptable.quality++;

        QualitySettings.SetQualityLevel((int)settingsDataScriptable.quality);
    }

    public void ChangeMusicVolume(float volume)
    {
        if (AudioManager.Instance == null) return;

        settingsDataScriptable.music = Mathf.Lerp(0f, 0.5f, volume);
        AudioManager.Instance.UpdateAudioSettings();
    }

    public void ChangeEffectsVolume(float volume)
    {
        if (AudioManager.Instance == null) return;

        settingsDataScriptable.effects = Mathf.Lerp(0f, 0.5f, volume);
        AudioManager.Instance.UpdateAudioSettings();
    }

    public void ChangeVibrations(bool state)
    {
        settingsDataScriptable.vibrations = state;
        //TODO: Vibrator class
        //Handheld.Vibrate();
    }
    #endregion
}