using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [Header("Gate References")]
    [SerializeField] private TextMeshProUGUI leftDisplay;
    [SerializeField] private TextMeshProUGUI rightDisplay;
    [Space]
    [SerializeField] private Renderer leftGate;
    [SerializeField] private Renderer rightGate;

    private FlockBehaviour flock;

    private float leftValue;
    private float rightValue;
    private OperationType leftOperation;
    private OperationType rightOperation;

    private const int GATE_MAX_VALUE = 20;
    private bool isActive = true;

    private readonly Dictionary<OperationType, string> operationSign = new Dictionary<OperationType, string>
    {
        {OperationType.Addition,        "+" },
        {OperationType.Subtraction,     "-" },
    };

    private void Awake()
    {
        flock = FindAnyObjectByType<FlockBehaviour>();
        if (flock == null) Debug.LogWarning($"[{gameObject.name}] Flock not found! (missing FlockBehaviour)");
    }

    private void OnEnable()
    {
        LevelManager.setupGame += GameSetup;
        GameSetup();
    }
    private void OnDisable()
    {
        LevelManager.setupGame -= GameSetup;
    }

    private void GameSetup()
    {
        rightOperation = RandomizeOperation();
        leftOperation = RandomizeOperation();

        rightValue = RandomizeValue();
        leftValue = RandomizeValue();

        leftDisplay.text = $"{operationSign[leftOperation]} {leftValue}";
        rightDisplay.text = $"{operationSign[rightOperation]} {rightValue}";

        leftDisplay.color = new Color(leftDisplay.color.r, leftDisplay.color.g, leftDisplay.color.b, 255f);
        rightDisplay.color = new Color(rightDisplay.color.r, rightDisplay.color.g, rightDisplay.color.b, 255f);

        var gateColor = rightGate.material.GetColor("_BaseColor");
        leftGate.material.SetColor("_BaseColor", new Color(gateColor.r, gateColor.g, gateColor.b, 1.0f));
        rightGate.material.SetColor("_BaseColor", new Color(gateColor.r, gateColor.g, gateColor.b, 1.0f));

        isActive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Agent agent) && isActive)
        {
            isActive = false;

            var value = (agent.transform.position.x - transform.position.x >= 0) ? rightValue : leftValue;
            var operation = (agent.transform.position.x - transform.position.x >= 0) ? rightOperation : leftOperation;
            flock.CalculateAgents(value, operation);

            DissolveGate(agent.transform.position.x - transform.position.x);

            if (AudioManager.Instance != null)
                AudioManager.Instance.Play((operation == OperationType.Addition)
                    ? AudioEffect.gate_add_agent
                    : AudioEffect.gate_remove_agent);
        }
    }

    //private OperationType RandomizeOperation() => (OperationType)Random.Range(0, 2);
    private OperationType RandomizeOperation()
    {
        var probability = 80f;
        var random = Random.Range(0, 99);

        if (random > probability)
            return OperationType.Subtraction;
        else
            return OperationType.Addition;
    }

    private float RandomizeValue() => Random.Range(1, GATE_MAX_VALUE);

    private async void DissolveGate(float side)
    {
        var color = leftGate.material.GetColor("_BaseColor");
        DOTween.To(() => color.a, o => color.a = o, 0.0f, 1.0f);

        //Left side
        if (side < 0)
        {
            while (color.a > 0)
            {
                leftDisplay.color = new Color(leftDisplay.color.r, leftDisplay.color.g, leftDisplay.color.b, color.a);
                leftGate.material.SetColor("_BaseColor", new Color(color.r, color.g, color.b, color.a));

                await Task.Yield();
            }

            leftDisplay.text = string.Empty;
        }

        //Right side
        else if (side > 0)
        {
            while (color.a > 0)
            {
                rightDisplay.color = new Color(leftDisplay.color.r, leftDisplay.color.g, leftDisplay.color.b, color.a);
                rightGate.material.SetColor("_BaseColor", new Color(color.r, color.g, color.b, color.a));
                await Task.Yield();
            }

            rightDisplay.text = string.Empty;
        }
    }
}