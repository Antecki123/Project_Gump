using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField] private AudioEffect effect;

    public void Play()
    {
        if (AudioManager.Instance)
            AudioManager.Instance.Play(effect);
    }
}