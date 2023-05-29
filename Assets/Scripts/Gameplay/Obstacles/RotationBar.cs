using UnityEngine;

[SelectionBase]
public class RotationBar : Obstacle
{
    [Header("Component Settings")]
    [SerializeField] private float rotationSpeed = 30.0f;

    private void OnEnable() => StartPosition();

    public override void OnUpdate()
    {
        obstacleTransform.Rotate(rotationSpeed * Time.deltaTime * transform.up);
    }
}