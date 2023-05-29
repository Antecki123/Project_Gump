using UnityEngine;

public class Finish : MonoBehaviour
{
    public bool Finished { get; private set; } = false;

    private void OnEnable() => LevelManager.setupGame += GameSetup;
    private void OnDisable() => LevelManager.setupGame -= GameSetup;

    private void GameSetup()
    {
        Finished = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Agent _))
        {
            Finished = true;
        }
    }
}