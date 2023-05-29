using TMPro;
using UnityEngine;

public class HudUpdater : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private FloatVariable variable;

    private void Update()
    {
        if (variable.value != int.Parse(text.text))
            text.text = Mathf.Floor(variable.value).ToString();
    }
}