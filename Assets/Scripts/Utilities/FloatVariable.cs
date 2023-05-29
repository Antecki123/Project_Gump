using UnityEngine;

[CreateAssetMenu(fileName = "New Float", menuName = "Scriptable Objects/Utilities/Float Variable")]
public class FloatVariable : ScriptableObject
{
    public float value;
    [TextArea] public string discription;
}