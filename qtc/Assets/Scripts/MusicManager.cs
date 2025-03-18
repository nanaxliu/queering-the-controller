using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public AudioSource backgroundMusic;
    public float fadeDuration = 5f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps music across scenes
            backgroundMusic.volume = 0; // Starts with no volume
            StartCoroutine(FadeInMusic()); // Starts fade-in effect
        }
        else
        {
            Destroy(gameObject); // Prevents duplicate music managers
        }
    }

    IEnumerator FadeInMusic()
    {
        float targetVolume = 0.5f;
        float currentTime = 0;

        backgroundMusic.Play(); // Starts playing music

        while (currentTime < fadeDuration)
        {
            backgroundMusic.volume = Mathf.Lerp(0, targetVolume, currentTime / fadeDuration);
            currentTime += Time.deltaTime;
            yield return null;
        }

        backgroundMusic.volume = targetVolume; // Ensures it reaches full volume
    }

    public void PlayMusic()
    {
        if (!backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }
    }

    public void StopMusic()
    {
        if (backgroundMusic.isPlaying)
        {
            backgroundMusic.Stop();
        }
    }

    public void ChangeMusic(AudioClip newClip)
    {
        if (backgroundMusic.clip != newClip)
        {
            backgroundMusic.clip = newClip;
            backgroundMusic.Play();
        }
    }
}
