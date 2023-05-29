using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Scriptable Objects/Utilities/Settings")]
public class GameSettingsScriptable : ScriptableObject
{
    [Header("Audio Settings")]
    [Range(0, 1)] public float music = 1;
    [Range(0, 1)] public float effects = 1;

    [Header("Graphis Settings")]
    public GraphicQuality quality;

    [Header("Miscellaneous")]
    public Language language;
    public bool vibrations = true;
    [Space]
    public StringVariable playerName;
}

public enum GraphicQuality
{
    Low,
    Medium,
    High,
}

public enum Language
{
    English,
    Polish,
    Italian,
    Spanish,
    French,
    German,
}