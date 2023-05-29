using UnityEngine;

[SelectionBase]
public abstract class Obstacle : MonoBehaviour
{
    private const float RANGE_MIN = -4.0f;
    private const float RANGE_MAX = 4.0f;

    protected Transform obstacleTransform;

    private void Awake() => obstacleTransform = transform;

    public abstract void OnUpdate();

    protected virtual void StartPosition()
    {
        var positionX = Random.Range(RANGE_MIN, RANGE_MAX);
        obstacleTransform.position = new Vector3(positionX, obstacleTransform.position.y, obstacleTransform.position.z);
    }

    private void Update()
    {
        foreach (var obstacle in ObstaclesFactory.Obstacles)
        {
            obstacle.OnUpdate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IKillable killable))
        {
            killable.Kill();
        }
    }
}