using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public static List<Chunk> ActiveChunks { get; private set; } = new List<Chunk>();

    [field: SerializeField] public ChunkType Type { get; private set; }
    [SerializeField] private List<Transform> obstaclesTransforms;

    private void OnEnable()
    {
        ActiveChunks.Add(this);
        AddObstacles();
    }
    private void OnDisable()
    {
        ActiveChunks.Remove(this);
        RemoveObstacles();
    }

    private void AddObstacles()
    {
        if (transform.position == Vector3.zero) return;

        foreach (var obstacle in obstaclesTransforms)
            ObstaclesFactory.OnCallObstacle?.Invoke(obstacle);
    }

    private void RemoveObstacles()
    {
        foreach (var obstacle in obstaclesTransforms)
        {
            var objectToDestroy = obstacle.GetComponentInChildren<Obstacle>();

            if (objectToDestroy != null)
                ObstaclesFactory.OnReturnObstacle?.Invoke(objectToDestroy);
        }
    }
}

public enum ChunkType
{
    Forest,
    Medieval,
    //WildWest,
}