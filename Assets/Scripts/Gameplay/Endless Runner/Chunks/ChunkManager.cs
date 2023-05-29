using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(20)]
public class ChunkManager : MonoBehaviour
{
    [SerializeField] private ChunkType activeType;
    [SerializeField] private WeatherType actualWeather;
    [Space]
    [SerializeField] GameObject rainEffect;
    [SerializeField] Light mainLight;

    private FlockController agent;
    private Weather weather;

    private readonly int typeChangeInterval = 20;
    private readonly int weatherChangeInterval = 10;
    private readonly int initialChunkCount = 4;
    private readonly float chunkSize = 50.0f;

    private int chunkPosition = 0;

    private void Awake()
    {
        agent = FindFirstObjectByType<FlockController>();
        weather = new Weather(rainEffect, mainLight);
    }

    private void OnEnable() => LevelManager.setupGame += GameSetup;
    private void OnDisable() => LevelManager.setupGame -= GameSetup;

    private void GameSetup()
    {
        chunkPosition = 0;
        activeType = ChunkType.Forest;
        actualWeather = WeatherType.Sunny;
        CancelInvoke();

        var chunkToReturn = new List<Chunk>(Chunk.ActiveChunks);

        foreach (var chunk in chunkToReturn)
        {
            ChunksFactory.OnReturnChunk?.Invoke(chunk);
        }

        for (int i = 0; i < initialChunkCount; i++)
        {
            var position = chunkSize * i * Vector3.forward;
            ChunksFactory.OnCallChunk?.Invoke(position, Quaternion.identity, activeType);

            chunkPosition++;
        }

        InvokeRepeating(nameof(UpdateChunks), 1.0f, 1.0f);
    }

    private void UpdateChunks()
    {
        var bufforChunkList = new List<Chunk>(Chunk.ActiveChunks);
        foreach (var chunk in bufforChunkList)
        {
            var distance = Mathf.Abs(chunk.transform.position.z - agent.transform.position.z);
            var direction = chunk.transform.position.z - agent.transform.position.z < 0;

            // Spawn new chunk
            if (distance > chunkSize && direction)
            {
                var chunkTransform = chunkSize * chunkPosition * Vector3.forward;

                activeType = RandomChunkType();
                weather.SetWeather(RandomWeather(), chunkTransform);

                ChunksFactory.OnCallChunk?.Invoke(chunkTransform, Quaternion.identity, activeType);
                ChunksFactory.OnReturnChunk?.Invoke(chunk);

                chunkPosition++;
            }
        }
    }

    private ChunkType RandomChunkType()
    {
        var newBiom = activeType;

        if (chunkPosition % typeChangeInterval == 0)
        {
            while (newBiom == activeType)
            {
                newBiom = (ChunkType)Random.Range(0, System.Enum.GetNames(typeof(ChunkType)).Length);
            }
        }

        return newBiom;
    }

    private WeatherType RandomWeather()
    {
        var newWeather = actualWeather;

        if (chunkPosition % weatherChangeInterval == 0)
        {
            while (newWeather == actualWeather)
            {
                newWeather = (WeatherType)Random.Range(0, System.Enum.GetNames(typeof(WeatherType)).Length);
            }

            actualWeather = newWeather;
        }

        return newWeather;
    }
}