using UnityEngine;

public class SpikeBar : Obstacle
{
    [Header("Component Settings")]
    [SerializeField] private float rotationSpeed = 50.0f;
    [Space]
    [SerializeField] private bool moving = false;
    [SerializeField] private float movementSpeed = 50.0f;
    [SerializeField] private float movementRange = 3.0f;

    private Vector3 startPosition;

    private void OnEnable()
    {
        startPosition = obstacleTransform.position;
        moving = Random.Range(0, 2) % 2 == 0;

        if (moving) StartPosition();
    }

    public override void OnUpdate()
    {
        obstacleTransform.Rotate(rotationSpeed * Time.deltaTime * transform.up);

        if (moving)
        {
            obstacleTransform.position = new Vector3(startPosition.x + Mathf.Cos(Time.time * movementSpeed / movementRange) * movementRange,
                obstacleTransform.position.y, obstacleTransform.position.z);
        }
    }
}