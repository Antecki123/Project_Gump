using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AgentsFactory))]
public class AgentsFactoryEditor : Editor
{
    private const float RING_RADIOUS = 1.5f;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Spawn Agents (EDITOR ONLY)", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Add 1 Agent"))
        {
            AddAgents(1);
        }
        if (GUILayout.Button("Remove 1 Agent"))
        {
            RemoveAgents(1);
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Add 5 Agent"))
        {
            AddAgents(5);

        }
        if (GUILayout.Button("Remove 5 Agent"))
        {
            RemoveAgents(5);
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Add 25 Agent"))
        {
            AddAgents(25);
        }
        if (GUILayout.Button("Remove 25 Agent"))
        {
            RemoveAgents(25);
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    private void AddAgents(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var position = Vector3.zero + Random.insideUnitSphere * RING_RADIOUS;
            var rotation = Quaternion.identity;

            AgentsFactory.OnAddAgent?.Invoke(position, rotation);
        }
    }

    private void RemoveAgents(int counter)
    {
        for (int i = 0; i < counter; i++)
        {
            if (Agent.ActiveAgents.Count > 0)
                AgentsFactory.OnRemoveAgent?.Invoke(FindAnyObjectByType<Agent>());
        }
    }
}