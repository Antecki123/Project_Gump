using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Component Settings")]
    [SerializeField] private float rotationSpeed = 100.0f;

    public static List<Coin> Coins { get; private set; } = new List<Coin>();

    private void OnEnable() =>LevelManager.setupGame += GameSetup;
    private void OnDisable() => LevelManager.setupGame -= GameSetup;

    private void Start() => Coins.Add(this);
    private void OnDestroy() => Coins.Remove(this);

    private void Update()
    {
        transform.Rotate(new Vector3(0.0f, rotationSpeed, 0.0f) * Time.deltaTime);
    }

    private void GameSetup()
    {
        foreach (var c in Coins)
        {
            c.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.TryGetComponent(out ICollectable collector))
        {
            collector.Collect();
            gameObject.SetActive(false);
        }*/
    }
}