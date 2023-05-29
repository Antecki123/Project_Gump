using UnityEngine;

public class ObjectFollower : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;
    private Transform followerTransform;

    [Header("Component Offset")]
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;

    private void Awake()
    {
        followerTransform = transform;
    }

    private void Update()
    {
        if (objectToFollow != null)
        {
            var position = new Vector3(0.0f, objectToFollow.position.y, objectToFollow.position.z);
            followerTransform.SetPositionAndRotation(position + positionOffset, Quaternion.Euler(rotationOffset));
        }
    }
}