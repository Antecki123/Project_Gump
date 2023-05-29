using UnityEngine;

[SelectionBase]
public class FlockController : MonoBehaviour
{
    [Header("Flock Control")]
    [SerializeField, Min(0)] private float movementSpeed = 5.0f;
    [SerializeField, Min(0)] private float traverseSpeed = 5.0f;

    private InputActions inputControls;
    private Transform flockTransform;
    private float travers;

    private readonly float pathBreadth = 5.0f;

    private void Awake() => flockTransform = transform;

    private void Start()
    {
        inputControls = new InputActions();
        inputControls.Enable();

        inputControls.PC.Movement.performed += ctx => travers = ctx.ReadValue<float>();
        inputControls.PC.Movement.canceled += ctx => travers = 0.0f;
    }


    private void Update()
    {
        flockTransform.position += movementSpeed * Time.deltaTime * flockTransform.forward;

#if UNITY_STANDALONE

#endif

#if UNITY_ANDROID

#endif

        if (Mathf.Abs(flockTransform.position.x) <= pathBreadth)
            flockTransform.position += travers * traverseSpeed * Time.deltaTime * Vector3.right;

        if (flockTransform.position.x > pathBreadth)
            flockTransform.position = new Vector3(pathBreadth, flockTransform.position.y, flockTransform.position.z);
        if (flockTransform.position.x < -pathBreadth)
            flockTransform.position = new Vector3(-pathBreadth, flockTransform.position.y, flockTransform.position.z);
    }

    public void SetSpeed(float speed) => movementSpeed = speed;
}