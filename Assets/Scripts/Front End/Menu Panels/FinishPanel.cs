using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FrontEnd
{
    public class FinishPanel : MenuPanel
    {
        [Header("Component References")]
        [SerializeField] private FloatVariable score;
        [SerializeField] private StringVariable playerTag;
        [Space]
        [SerializeField] private TextMeshProUGUI resultText;
        [Space]
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button confirmButton;

        private string input;
        private int finalScore;

        private void OnEnable() => LevelManager.finishGame += OpenPanel;
        private void OnDisable() => LevelManager.finishGame -= OpenPanel;

        #region BUTTONS
        public void ConfirmButton()
        {
            var leaderboard = FindAnyObjectByType<LeaderboardView>();

            if (leaderboard != null)
                leaderboard.AddNewRecord(new LeaderboardRecord() { name = input, value = finalScore });

            LevelManager.setupGame?.Invoke();
            UIManager.onOpenFrontEnd?.Invoke();
        }
        #endregion

        public void ReadInput(string input)
        {
            this.input = input;

            confirmButton.interactable = input != string.Empty;
        }

        private void OpenPanel()
        {
            finalScore = (int)Mathf.Floor(score.value);
            resultText.text = $"- {finalScore} -";

            confirmButton.interactable = false;

            if (AudioManager.Instance != null)
                AudioManager.Instance.Play(AudioEffect.game_over);
        }
    }
}