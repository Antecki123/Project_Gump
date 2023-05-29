using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(10)]
public class AgentsFactory : MonoBehaviour
{
    public static Action<Vector3, Quaternion> OnAddAgent;
    public static Action<Agent> OnRemoveAgent;

    [Header("Component References")]
    [SerializeField] private int objectsMaxAmount = 250;
    [SerializeField] private AgentsStorage agentsStorage;

    private readonly Queue<Agent> availableAgents = new Queue<Agent>();
    private Transform flockParent;

    private void Awake()
    {
        flockParent = FindAnyObjectByType<FlockBehaviour>().transform;
    }

    private void OnEnable()
    {
        OnAddAgent += CreateAgent;
        OnRemoveAgent += ReturnAgent;
    }
    private void OnDisable()
    {
        OnAddAgent -= CreateAgent;
        OnRemoveAgent -= ReturnAgent;
    }

    private void Start()
    {
        for (int i = 0; i < objectsMaxAmount; i++)
        {
            var newAgent = Instantiate(agentsStorage.activeAgent);
            newAgent.transform.SetParent(flockParent);
            newAgent.gameObject.SetActive(false);

            availableAgents.Enqueue(newAgent);
        }
    }

    private void CreateAgent(Vector3 position, Quaternion rotation)
    {
        if (Agent.ActiveAgents.Count >= objectsMaxAmount) return;
        var newAgent = availableAgents.Dequeue();

        newAgent.transform.SetPositionAndRotation(position, rotation);
        newAgent.transform.SetParent(flockParent);
        newAgent.gameObject.SetActive(true);
    }

    private void ReturnAgent(Agent returned)
    {
        availableAgents.Enqueue(returned);

        returned.transform.SetParent(flockParent);
        returned.gameObject.SetActive(false);
    }
}