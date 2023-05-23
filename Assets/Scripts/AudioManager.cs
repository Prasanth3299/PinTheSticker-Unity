using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource MusicSource;

    // Singleton instance.
    public static AudioManager Instance = null;
    public static bool isMusicPlaying = false;

    // Initialize the singleton instance.

    private void Awake()
    {
        // If there is not already an instance of SoundManager, set it to this.
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    public void SetMusicFlag(bool flag)
    {
        isMusicPlaying = flag;
    }

    public bool GetMusicFlag()
    {
        return isMusicPlaying;
    }

   




    // Play a single clip through the sound effects source.
    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void StopSound()
    {
        audioSource.Stop();
    }



    // Play a single clip through the music source.
    public void PlayMusic(AudioClip clip)
    {
        MusicSource.clip = clip;
        MusicSource.Play();
    }

    public void StopMusic()
    {
        MusicSource.Stop();
    }

    public void PauseMusic()
    {
        if (MusicSource.isPlaying)
        {
            MusicSource.Pause();
        }
    }

    public void UnPauseMusic()
    {
        if (GetMusicFlag())
        {
            MusicSource.UnPause();
        }
    }





}










