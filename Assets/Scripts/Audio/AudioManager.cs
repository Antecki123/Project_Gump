using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Sources")]
    [SerializeField] private AudioSource effectsSource;
    [SerializeField] private AudioSource musicSource;
    [Space]
    [SerializeField] private AudioLibrary audioLibrary;
    [SerializeField] private GameSettingsScriptable settings;
    [Space]
    [SerializeField] private bool debugLogs = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start() => UpdateAudioSettings();

    /// <summary>
    /// Play effect sound once.
    /// </summary>
    /// <param name="audio"></param>
    public void Play(AudioEffect audio)
    {
        if(debugLogs)
            Debug.Log($"[Audio Manager] Audio name: {audio}.");

        var audioToPlay = audioLibrary.FindAudio(audio);
        effectsSource.PlayOneShot(audioToPlay);
    }

    /// <summary>
    /// Play background music in loop.
    /// </summary>
    /// <param name="audio"></param>
    public async void Play(AudioSoundtrack audio)
    {
        if (debugLogs)
            Debug.Log($"[Audio Manager] Audio name: {audio}.");

        var audioToPlay = audioLibrary.FindAudio(audio);
        var audioChangeDuration = 1.0f;

        musicSource.DOFade(0.0f, audioChangeDuration);
        await Task.Delay((int)(audioChangeDuration * 1000));

        musicSource.loop = true;
        musicSource.clip = audioToPlay;

        musicSource.Play();
        musicSource.DOFade(settings.music, audioChangeDuration);
    }

    /// <summary>
    /// Update audio volume according to game settings.
    /// </summary>
    public void UpdateAudioSettings()
    {
        musicSource.volume = settings.music;
        effectsSource.volume = settings.effects;
    }
}