using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Agent : MonoBehaviour, IKillable
{
    public static List<AgentProperties> AgentProperties { get; private set; } = new List<AgentProperties>();

    public static List<Agent> ActiveAgents { get; private set; } = new List<Agent>();
    public static List<Rigidbody> AgentsRigidbodys { get; private set; } = new List<Rigidbody>();

    [Header("Component References")]
    public AgentData agentData;

    private Rigidbody rigidBody;
    private Transform agentTransform;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        agentTransform = transform;
    }

    private void OnEnable()
    {
        AgentProperties.Add(new AgentProperties
        {
            agent = this,
            rigidbody = rigidBody,
        });

        ActiveAgents.Add(this);
        AgentsRigidbodys.Add(rigidBody);
        agentTransform.position = new Vector3(agentTransform.position.x, 0.0f, agentTransform.position.z);
    }
    private void OnDisable()
    {
        AgentProperties.Remove(AgentProperties.Find(a => a.agent == this));

        ActiveAgents.Remove(this);
        AgentsRigidbodys.Remove(rigidBody);
    }

    public void Kill()
    {
        if (agentData.deathEffect != null)
            Instantiate(agentData.deathEffect, agentTransform.position, agentTransform.rotation);

        if (AudioManager.Instance != null)
            AudioManager.Instance.Play(AudioEffect.agent_destroy);

        AgentsFactory.OnRemoveAgent?.Invoke(this);
    }
}

public struct AgentProperties
{
    public Agent agent;
    public Rigidbody rigidbody;
}