using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationManager
{
    [SerializeField] private bool debugLogs = false;

    private readonly Dictionary<Language, int> locales;

    public LocalizationManager(Language language)
    {
        locales = new Dictionary<Language, int>()
        {
            { Language.English,     0},
            { Language.French,      1},
            { Language.German,      2},
            { Language.Italian,     3},
            { Language.Polish,      4},
            { Language.Spanish,     5},
        };

        SetLocale(language);
    }

    public async void SetLocale(Language language)
    {
        if (debugLogs)
            Debug.Log($"[Localization] Language: {language}.");

        await LocalizationSettings.InitializationOperation.Task;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[locales[language]];
    }
}