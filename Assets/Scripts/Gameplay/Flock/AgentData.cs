using UnityEngine;

[CreateAssetMenu(fileName = "Agent Data", menuName = "Scriptable Objects/Game Data/Agent Data")]
public class AgentData : ScriptableObject
{
    public float velocity = 1.0f;
    public GameObject deathEffect;
}