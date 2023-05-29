using UnityEngine;
using UnityEngine.UI;

namespace FrontEnd
{
    [RequireComponent(typeof(Button))]
    public class PanelSwitcher : MonoBehaviour
    {
        [Header("Panel Settings")]
        [SerializeField] private UIPanelName panel;

        private UIManager manager;

        private void Awake()
        {
            manager = FindFirstObjectByType<UIManager>();
        }

        private void Start()
        {
            var button = GetComponent<Button>();
            button.onClick.AddListener(() => OpenPanel());
        }

        public void OpenPanel()
        {
            if (manager != null)
                manager.OpenPanel(panel);
        }
    }
}