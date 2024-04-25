using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;

    public AudioSource gameMusic;

    public AudioClip[] musicOptoins;

    public AudioSource endGameMusic;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // This ensures that this object persists across scene changes
        }
        else
        {
            Destroy(gameObject); // If another AudioManager already exists, destroy this one
        }
    }

    public void ChangeMusic(int index)
    {
        Debug.Log("music for level "+index + " is playing. change options in audio manager in start scene.");
        gameMusic.clip = musicOptoins[index];
        gameMusic.Play();
    }

    public void ToggleMute()
    {
        gameMusic.mute = !gameMusic.mute;
    }

    public void PlayCountDownMusic()
    {

        endGameMusic.Play();
    }
}
