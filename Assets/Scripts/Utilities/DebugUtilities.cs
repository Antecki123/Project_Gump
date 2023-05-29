using UnityEngine;
using UnityEngine.UI;

public class DebugUtilities : MonoBehaviour
{
    [SerializeField] private Text FPSCounter;

    private void Start()
    {
        InvokeRepeating(nameof(CalculateFPS), 0.0f, 0.5f);
    }

    private void CalculateFPS()
    {
        FPSCounter.text = $"FPS: {(int)(1.0f / Time.unscaledDeltaTime)}";
    }
}