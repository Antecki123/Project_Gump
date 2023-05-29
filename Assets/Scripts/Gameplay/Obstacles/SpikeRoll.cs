using UnityEngine;

public class SpikeRoll : Obstacle
{
    [Header("Component Settings")]
    [SerializeField] private float rotationSpeed = 50.0f;

    private void OnEnable() => StartPosition();

    public override void OnUpdate()
    {
        obstacleTransform.Rotate(rotationSpeed * Time.deltaTime * transform.right);
    }
}