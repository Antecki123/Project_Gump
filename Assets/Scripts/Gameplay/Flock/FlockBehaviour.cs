using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(20)]
public class FlockBehaviour : MonoBehaviour
{
    private const float RING_RADIOUS = 1.5f;
    private const float GRAVITY = 9.81f;

    private void OnEnable() => LevelManager.setupGame += SetupFlock;
    private void OnDisable() => LevelManager.setupGame -= SetupFlock;

    private void SetupFlock()
    {
        transform.position = Vector3.zero;

        var agentsToRemove = new List<Agent>(Agent.ActiveAgents);

        foreach (var agent in agentsToRemove)
        {
            AgentsFactory.OnRemoveAgent(agent);
        }

        AgentsFactory.OnAddAgent?.Invoke(Vector3.zero, Quaternion.identity);
    }

    private void Update()
    {
        foreach (var agent in Agent.AgentsRigidbodys)
        {
            agent.velocity = -GRAVITY * transform.up;
        }
    }

    #region Gates calculations
    public void CalculateAgents(float gateValue, OperationType operation)
    {
        if (operation == OperationType.Addition)
        {
            AddAgents((int)gateValue);
        }

        else if (operation == OperationType.Subtraction)
        {
            RemoveAgents((int)gateValue);
        }
    }

    private void AddAgents(int count)
    {
        if (Agent.ActiveAgents.Count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                var position = transform.position + Random.insideUnitSphere * RING_RADIOUS;
                var rotation = Quaternion.identity;

                AgentsFactory.OnAddAgent?.Invoke(position, rotation);
            }
        }
    }

    private void RemoveAgents(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (Agent.ActiveAgents.Count <= 0)
                break;

            var randomAgent = Agent.ActiveAgents[Random.Range(0, Agent.ActiveAgents.Count)];
            randomAgent.Kill();
        }
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RING_RADIOUS);
    }
}

public enum OperationType
{
    Addition,
    Subtraction,
}