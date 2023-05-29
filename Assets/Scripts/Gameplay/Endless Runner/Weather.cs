using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Weather
{
    [Header("Weather References")]
    private GameObject rainEffect;
    private Light light;

    WeatherType actualWeather = WeatherType.Sunny;

    private Vector3 chunkPosition;
    private List<GameObject> rainEffectsContainer = new List<GameObject>();

    [Header("Light Colors")]
    private readonly Color clearSkyColor = new Color(1f, 1f, 1f);
    private readonly Color rainSkyColor = new Color(0.6f, 0.6f, 0.5f);
    private readonly Color fogSkyColor = new Color(0.6f, 0.6f, 0.5f);
    private readonly Color nightSkyColor = new Color(0.7f, 0.7f, 0.7f);

    private readonly float fadeTime = 5.0f;
    private readonly float setupFadeTime = 1.0f;

    public Weather(GameObject rainEffect, Light light)
    {
        LevelManager.setupGame += SetupGame;

        this.rainEffect = rainEffect;
        this.light = light;
    }

    public void SetWeather(WeatherType weather, Vector3 position)
    {
        chunkPosition = position;

        if (weather == actualWeather) return;

        switch (weather)
        {
            case WeatherType.Sunny:
                Sunny();
                break;
            case WeatherType.Rain:
                Rain();
                break;
            case WeatherType.Fog:
                Fog();
                break;
            case WeatherType.Night:
                Night();
                break;
            default:
                break;
        }
    }

    private void SetupGame()
    {
        Debug.Log("Setup Game");

        light.DOIntensity(1.0f, setupFadeTime);
        light.DOColor(clearSkyColor, setupFadeTime);

        DOTween.To(() => RenderSettings.fogStartDistance, w => RenderSettings.fogStartDistance = w, 50f, setupFadeTime).SetUpdate(true);
        DOTween.To(() => RenderSettings.fogEndDistance, w => RenderSettings.fogEndDistance = w, 150f, setupFadeTime).SetUpdate(true);

        DOTween.To(() => RenderSettings.reflectionIntensity, w => RenderSettings.reflectionIntensity = w, 1.0f, setupFadeTime);
        DOTween.To(() => RenderSettings.ambientIntensity, w => RenderSettings.ambientIntensity = w, 1.0f, setupFadeTime);

        actualWeather = WeatherType.Sunny;
    }

    private void Sunny()
    {
        Debug.Log("Sunny");

        light.DOIntensity(1.0f, fadeTime);
        light.DOColor(clearSkyColor, fadeTime);

        DOTween.To(() => RenderSettings.fogStartDistance, w => RenderSettings.fogStartDistance = w, 50f, fadeTime);
        DOTween.To(() => RenderSettings.fogEndDistance, w => RenderSettings.fogEndDistance = w, 150f, fadeTime);

        DOTween.To(() => RenderSettings.reflectionIntensity, w => RenderSettings.reflectionIntensity = w, 1.0f, fadeTime);
        DOTween.To(() => RenderSettings.ambientIntensity, w => RenderSettings.ambientIntensity = w, 1.0f, fadeTime);

        actualWeather = WeatherType.Sunny;
    }

    private void Rain() // WiP
    {
        Debug.Log("Rain");
        /*
        var rain = Object.Instantiate(rainEffect);
        rainEffectsContainer.Add(rain);
        rain.transform.position = chunkPosition + Vector3.up * 5f;
        rain.transform.localScale *= 3;

        light.DOIntensity(0.3f, fadeTime);
        light.DOColor(rainSkyColor, fadeTime);

        AudioManager.Instance.Play(AudioSoundtrack.theme_02);
        */
    }

    private void Fog()
    {
        Debug.Log("Fog");

        light.DOIntensity(0.0f, fadeTime);
        light.DOColor(fogSkyColor, fadeTime);

        DOTween.To(() => RenderSettings.fogStartDistance, w => RenderSettings.fogStartDistance = w, 0f, fadeTime);
        DOTween.To(() => RenderSettings.fogEndDistance, w => RenderSettings.fogEndDistance = w, 50f, fadeTime);

        DOTween.To(() => RenderSettings.reflectionIntensity, w => RenderSettings.reflectionIntensity = w, 0.5f, fadeTime);
        DOTween.To(() => RenderSettings.ambientIntensity, w => RenderSettings.ambientIntensity = w, 1.0f, fadeTime);

        actualWeather = WeatherType.Fog;
    }

    private void Night()
    {
        Debug.Log("Night");

        light.DOIntensity(0.1f, fadeTime);
        light.DOColor(nightSkyColor, fadeTime);

        DOTween.To(() => RenderSettings.fogStartDistance, w => RenderSettings.fogStartDistance = w, 20f, fadeTime);
        DOTween.To(() => RenderSettings.fogEndDistance, w => RenderSettings.fogEndDistance = w, 200f, fadeTime);

        DOTween.To(() => RenderSettings.reflectionIntensity, w => RenderSettings.reflectionIntensity = w, 0.2f, fadeTime);
        DOTween.To(() => RenderSettings.ambientIntensity, w => RenderSettings.ambientIntensity = w, 0.3f, fadeTime);

        actualWeather = WeatherType.Night;
    }
}

public enum WeatherType
{
    Sunny,
    Rain,
    Fog,
    Night,
}