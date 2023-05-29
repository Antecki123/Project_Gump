using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(10)]
public class ChunksFactory : MonoBehaviour
{
    public static Action<Vector3, Quaternion, ChunkType> OnCallChunk;
    public static Action<Chunk> OnReturnChunk;

    [SerializeField] private List<Chunk> chunkPrefabs;

    private List<Chunk> availableChunks = new List<Chunk>();

    private void OnEnable()
    {
        OnCallChunk += CallChunk;
        OnReturnChunk += ReturnChunk;
    }
    private void OnDisable()
    {
        OnCallChunk -= CallChunk;
        OnReturnChunk -= ReturnChunk;
    }

    private void Start()
    {
        foreach (var chunk in chunkPrefabs)
            chunk.gameObject.SetActive(false);

        for (int i = 0; i < chunkPrefabs.Count; i++)
        {
            var chunkObject = Instantiate(chunkPrefabs[i]);
            chunkObject.transform.SetParent(transform);
            chunkObject.gameObject.SetActive(false);
            chunkObject.name = $"Chunk {i}";

            availableChunks.Add(chunkObject);
        }
    }

    private void CallChunk(Vector3 position, Quaternion rotation, ChunkType type)
    {
        //TODO: something is no yes
        var randomChunk = availableChunks[UnityEngine.Random.Range(0, availableChunks.Count)];

        while (randomChunk.Type != type)
        {
            randomChunk = availableChunks[UnityEngine.Random.Range(0, availableChunks.Count)];
        }

        randomChunk.transform.SetPositionAndRotation(position, rotation);
        randomChunk.gameObject.SetActive(true);

        availableChunks.Remove(randomChunk);
    }

    private void ReturnChunk(Chunk returned)
    {
        availableChunks.Add(returned);

        returned.transform.SetParent(transform);
        returned.gameObject.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        foreach (var chunk in chunkPrefabs)
            chunk.gameObject.SetActive(true);
    }
}