using DG.Tweening;
using FrontEnd;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Image loadingBar;
    [Space]
    [SerializeField] private List<GameScenes> gameScenes;

    private readonly float fadingTime = 2.0f;

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void Start()
    {
        SceneManager.LoadSceneAsync(SceneName.Static.ToString(), LoadSceneMode.Additive);

        var rotationTime = 2.0f;
        loadingBar.transform.DORotate(new Vector3(0f, 0f, -360f), rotationTime, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetRelative()
            .SetEase(Ease.Linear);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameScenes.Find(s => s.name.ToString() == scene.name.ToString()).isLoaded = true;

        foreach (var s in gameScenes)
            if (!s.isLoaded) return;

        InitializeGame();
    }

    private async void InitializeGame()
    {
        await System.Threading.Tasks.Task.Delay((int)fadingTime * 1000);

        background.DOFade(0.0f, fadingTime);
        loadingBar.DOFade(0.0f, fadingTime);

        await System.Threading.Tasks.Task.Delay((int)fadingTime * 1000);
        UIManager.onOpenFrontEnd?.Invoke();
    }
}

public enum SceneName
{
    GUI,
    Gameplay,
    Static,
}

[Serializable]
public class GameScenes
{
    public SceneName name;
    public bool isLoaded;
}