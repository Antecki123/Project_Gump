using System;
using UnityEngine;
using UnityEngine.UI;

namespace FrontEnd
{
    public class AgentSelectButton : MonoBehaviour
    {
        [Header("Component References")]
        [SerializeField] private AgentsStorage agentsStorage;
        [SerializeField] private string agentName;
        
        private Button button;

        private void Start()
        {
            name = agentName;

            button = GetComponent<Button>();
            button.onClick.AddListener(() => OnClick());
        }

        private void OnClick()
        {
            agentsStorage.activeAgent = Array.Find(agentsStorage.agents, a => a.agentName == agentName).agentPrefab;
        }
    }
}