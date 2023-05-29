using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FrontEnd
{
    public class SettingsView : MenuPanel
    {
        [Header("Database")]
        [SerializeField] private GameSettingsScriptable settingsData;

        [Header("Panel Objects")]
        [SerializeField] private TextMeshProUGUI languageText;
        [SerializeField] private TextMeshProUGUI graphicText;
        [Space]
        [SerializeField] private Slider musicVolume;
        [SerializeField] private Slider effectsVolume;
        [SerializeField] private Toggle vibrations;

        [Header("Panels")]
        [SerializeField] private MenuPanel creditsPanel;

        private GameSettings settings;

        #region BUTTONS
        public void SaveChanges()
        {
            if (settingsData)
                settings.SaveSettings();
        }
        public void OpenCreditsPanel()
        {
            if (creditsPanel)
                creditsPanel.SetActive(true);
        }
        #endregion

        private void Awake()
        {
            settings = new GameSettings(settingsData);
            settings.LoadSettings();
        }

        protected override void Enable()
        {
            languageText.text = settingsData.language.ToString();
            graphicText.text = settingsData.quality.ToString();

            musicVolume.value = Mathf.Lerp(0f, 2f, settingsData.music);
            effectsVolume.value = Mathf.Lerp(0f, 2f, settingsData.effects);
            vibrations.isOn = settingsData.vibrations;
        }

        public void DecreaseLanguage() => settings.DecreaseLanguage();
        public void IncreaseLanguage() => settings.IncreaseLanguage();

        public void DecreaseGraphicQuality()
        {
            settings.DecreaseGraphicQuality();
            graphicText.text = graphicText.text = settingsData.quality.ToString();
        }
        public void IncreaseGraphicQuality()
        {
            settings.IncreaseGraphicQuality();
            graphicText.text = graphicText.text = settingsData.quality.ToString();
        }

        public void ChangeMusicValue() => settings.ChangeMusicVolume(musicVolume.value);
        public void ChangeEffectsValue() => settings.ChangeEffectsVolume(effectsVolume.value);

        public void ChangeVibrations() => settings.ChangeVibrations(vibrations.isOn);
    }
}