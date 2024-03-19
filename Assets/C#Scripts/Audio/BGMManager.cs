using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : Singleton<BGMManager>
{
    [SerializeField] 
    private AudioClip[] bgmClips;
    private Dictionary<string, AudioClip> bgmDict;
    public string currentBGM;
    AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        bgmDict= new Dictionary<string, AudioClip>();
        foreach (AudioClip bgmClip in bgmClips)
            bgmDict[bgmClip.name] = bgmClip;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
    }

    private void Start()
    {
        PlayBGM("Main", 1, 1);
    }

    public void SwitchBGM(string bgmName, float fadeTime,float volume)
    {
        if (currentBGM == bgmName)
            return;
        StopCoroutine(nameof(FadeOut));
        StopCoroutine(nameof(FadeIn));
        StopCoroutine(nameof(FadeOutAndIn));
        StartCoroutine(FadeOutAndIn(bgmDict[bgmName], fadeTime,volume));
        currentBGM= bgmName;
    }

    private IEnumerator FadeOutAndIn(AudioClip newClip, float fadeTime, float volume)
    {
        float t = 0.0f;
        float startVolume = audioSource.volume;

        // Fade out current BGM
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0.0f, t / fadeTime);
            yield return null;
        }

        // Switch to new BGM and fade in
        audioSource.clip = newClip;
        audioSource.Play();
        t = 0.0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0.0f, volume, t / fadeTime);
            yield return null;
        }
    }

    public void StopBGM(float fadeTime)
    {
        StopCoroutine(nameof(FadeOut));
        StopCoroutine(nameof(FadeIn));
        StopCoroutine(nameof(FadeOutAndIn));
        StartCoroutine(FadeOut(fadeTime));
        currentBGM= null;
    }

    private IEnumerator FadeOut(float fadeTime)
    {
        float t = 0.0f;
        float startVolume = audioSource.volume;

        // Fade out current BGM
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0.0f, t / fadeTime);
            yield return null;
        }

        audioSource.Stop();
    }

    public void PlayBGM(string bgmName, float fadeTime, float volume)
    {
        if (currentBGM == bgmName)
            return;
        StopCoroutine(nameof(FadeOut));
        StopCoroutine(nameof(FadeIn));
        StopCoroutine(nameof(FadeOutAndIn));
        StartCoroutine(FadeIn(bgmDict[bgmName], fadeTime,volume));
        currentBGM= bgmName;
    }

    private IEnumerator FadeIn(AudioClip newClip, float fadeTime,float volume)
    {
        float t = 0.0f;
        float startVolume = 0.0f;

        // Switch to new BGM and fade in
        audioSource.clip = newClip;
        audioSource.Play();
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, volume, t / fadeTime);
            yield return null;
        }
    }

}
