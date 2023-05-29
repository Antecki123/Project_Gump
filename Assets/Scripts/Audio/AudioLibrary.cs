using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Library", menuName = "Scriptable Objects/Utilities/Audio Library")]
public class AudioLibrary : ScriptableObject
{
    public SoundTheme[] soundTheme;
    public SoundEffect[] soundEffect;

    public AudioClip FindAudio(AudioEffect audio)
    {
        return Array.Find(soundEffect, a => a.type == audio).clip;
    }

    public AudioClip FindAudio(AudioSoundtrack audio)
    {
        return Array.Find(soundTheme, a => a.type == audio).clip;
    }
}

[Serializable]
public class SoundTheme
{
    public AudioClip clip;
    public AudioSoundtrack type;
}

[Serializable]
public class SoundEffect
{
    public AudioClip clip;
    public AudioEffect type;
}