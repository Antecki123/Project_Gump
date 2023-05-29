using UnityEngine;

[CreateAssetMenu(fileName = "Agents Storage", menuName = "Scriptable Objects/Utilities/Agents Storage")]
public class AgentsStorage : ScriptableObject
{
    public Agent activeAgent;
    public AgentPrefab[] agents;
}

[System.Serializable]
public struct AgentPrefab
{
    public string agentName;
    public Agent agentPrefab;
    public Sprite agentSprite;

    public bool agentAvailability;
}