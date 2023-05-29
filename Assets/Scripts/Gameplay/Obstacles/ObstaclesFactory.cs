using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(10)]
public class ObstaclesFactory: MonoBehaviour
{
    public static List<Obstacle> Obstacles { get; private set; } = new List<Obstacle>();

    public static Action<Transform> OnCallObstacle;
    public static Action<Obstacle> OnReturnObstacle;

    [SerializeField] private List<Obstacle> obstaclePrefabs;

    private List<Obstacle> availableObstacles = new List<Obstacle>();

    private void OnEnable()
    {
        OnCallObstacle += CallObstacle;
        OnReturnObstacle += ReturnObstacle;
    }
    private void OnDisable()
    {
        OnCallObstacle -= CallObstacle;
        OnReturnObstacle -= ReturnObstacle;
    }

    private void Start()
    {
        foreach (var obstacle in obstaclePrefabs)
            obstacle.gameObject.SetActive(false);

        for (int i = 0; i < obstaclePrefabs.Count; i++)
        {
            var obstacleObject = Instantiate(obstaclePrefabs[i]);
            obstacleObject.transform.SetParent(transform);
            obstacleObject.gameObject.SetActive(false);

            availableObstacles.Add(obstacleObject);
        }
    }

    private void CallObstacle(Transform pivot)
    {
        var randomObstacle = availableObstacles[UnityEngine.Random.Range(0, availableObstacles.Count)];

        randomObstacle.transform.SetPositionAndRotation(pivot.position, pivot.rotation);
        randomObstacle.transform.SetParent(pivot);
        randomObstacle.gameObject.SetActive(true);

        Obstacles.Add(randomObstacle);
        availableObstacles.Remove(randomObstacle);
    }

    private void ReturnObstacle(Obstacle returned)
    {
        Obstacles.Remove(returned);
        availableObstacles.Add(returned);

        returned.transform.SetParent(transform);
        returned.gameObject.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        foreach (var obstacle in obstaclePrefabs)
            obstacle.gameObject.SetActive(true);
    }
}