using System;
using System.Collections.Generic;
using UnityEngine;

namespace FrontEnd
{
    [DefaultExecutionOrder(50)]
    public class UIManager : MonoBehaviour
    {
        public static Action onOpenFrontEnd;
        public static Action onCloseFrontEnd;

        private Dictionary<UIPanelName, MenuPanel> panelCollection;

        [Header("Panels References")]
        [SerializeField] private MenuPanel mainMenuPanel;
        [SerializeField] private MenuPanel settingsPanel;
        [SerializeField] private MenuPanel agentsSelectPanel;
        [SerializeField] private MenuPanel leaderboardPanel;
        [SerializeField] private MenuPanel creditsPanel;
        [Space]
        [SerializeField] private MenuPanel hudPanel;
        [SerializeField] private MenuPanel pausePanel;
        [SerializeField] private MenuPanel finishPanel;
        [Space]
        [SerializeField] private Fader fader;

        [Header("Data Persistence")]
        [SerializeField] private GameSettingsScriptable settings;

        [Header("Audio")]
        [SerializeField] private AudioSoundtrack soundtrack;

        private void OnEnable()
        {
            onOpenFrontEnd += OpenFrontEnd;
            onCloseFrontEnd += CloseFrontEnd;
            LevelManager.pauseGame += PauseGame;
            LevelManager.finishGame += FinishGame;
        }
        private void OnDisable()
        {
            onOpenFrontEnd -= OpenFrontEnd;
            onCloseFrontEnd -= CloseFrontEnd;
            LevelManager.pauseGame -= PauseGame;
            LevelManager.finishGame -= FinishGame;
        }

        private void Start()
        {
            panelCollection = new Dictionary<UIPanelName, MenuPanel>
            {
                { UIPanelName.MainMenu,         mainMenuPanel       },
                { UIPanelName.Settings,         settingsPanel       },
                { UIPanelName.Credits,          creditsPanel        },
                { UIPanelName.AgentSelector,    agentsSelectPanel   },
                { UIPanelName.Leaderboard,      leaderboardPanel    },
                { UIPanelName.PausePanel,       pausePanel          },
                { UIPanelName.FinishPanel,      finishPanel         },
            };
        }

        #region BUTTONS
        public void StartGameButton()
        {
            onCloseFrontEnd?.Invoke();
            LevelManager.startGame?.Invoke();
        }
        public void PauseGameButton()
        {
            LevelManager.pauseGame?.Invoke();
        }
        public void ExitButton()
        {
            Application.Quit();
        }
        #endregion

        public void OpenPanel(UIPanelName panel)
        {
            foreach (var p in panelCollection)
            {
                if (p.Value == null) continue;
                p.Value.SetActive(p.Key == panel);
            }
        }

        private void OpenFrontEnd()
        {
            if (AudioManager.Instance)
            {
                AudioManager.Instance.UpdateAudioSettings();
                AudioManager.Instance.Play(soundtrack);
            }

            // Fade screen
            fader.Fade(1.0f);

            // Deactivate game HUD
            hudPanel.SetActive(false);

            OpenPanel(UIPanelName.MainMenu);
        }

        private void CloseFrontEnd()
        {
            // Close all panels
            foreach (var p in panelCollection)
            {
                if (p.Value == null) continue;
                p.Value.SetActive(false);
            }

            // Clear fader
            fader.Fade(0.0f);

            // Activate game HUD
            hudPanel.SetActive(true);
        }

        private void PauseGame()
        {
            if (LevelManager.GameState == GameState.GamePaused)
                fader.Fade(1.0f);
            else
                fader.Fade(0.0f);

            hudPanel.SetActive(LevelManager.GameState != GameState.GamePaused);
            pausePanel.SetActive(LevelManager.GameState == GameState.GamePaused);
        }

        private void FinishGame()
        {
            hudPanel.SetActive(false);
            finishPanel.SetActive(true);

            fader.Fade(1.0f);
        }
    }

    public enum UIPanelName
    {
        MainMenu,
        SelectLevel,
        Settings,
        Leaderboard,
        ExitPopup,
        AgentSelector,
        PausePanel,
        FinishPanel,
        Credits,
    }
}