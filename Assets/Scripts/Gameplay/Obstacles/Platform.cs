using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Transform platform;
    [Space]
    [SerializeField] private float movementSpeed = 50.0f;
    [SerializeField] private float movementRange = 3.0f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = platform.position;
    }

    private void Update()
    {
        platform.position = new Vector3(startPosition.x + Mathf.Cos(Time.time * movementSpeed / movementRange) * movementRange,
                platform.position.y, platform.position.z);
    }
}