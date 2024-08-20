
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    public Sound[] sounds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(transform.root);
        }
        else
        {
            Destroy(gameObject);
        }


        foreach (Sound s in sounds) 
        {
            s.source = gameObject.AddComponent<AudioSource>();

            if (s.clips.Length > 1)
            {
                s.clipSelector = new AudioClipSelector();
            }

            if (s.clips.Length != 0)
            {
                s.source.clip = s.clips[0];
            }
            else
            {
                Debug.LogWarning($"Missing sound {s.name}!");
            }

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.looped;
            s.source.playOnAwake = true;
        }
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += ChangeBackgroundMusic;
    }

    private void Start()
    {
        // Subscribe background music controller to the OnSceneChange event.
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Main Menu")
        {
            PlaySound("MainMenu");
        }
    }

    private void ChangeBackgroundMusic(Scene scene, LoadSceneMode mode)
    {



        if (scene.name == "Main Menu")
        {
            PlaySound("MainMenu");
        }

    }

    /// <summary>
    /// Plays a sound of the specified name.
    /// </summary>
    /// <param name="name">Name of the sound to play.</param>
    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => name == sound.name);

        if (s == null)
        {
            Debug.LogWarning($"Missing sound {name}!");
            return;
        }

        if (s.clips.Length == 0)
        {
            Debug.LogWarning($"Missing clip for {name} sound!");
            return;
        }

        // Check for additional clips for the same sound, and if any randomise the choice.
        if(s.clipSelector != null)
        {
            s.source.clip = s.clipSelector.GetRandomAudioClip(s.clips);
        }
        if (s.source.isPlaying)
        {
            return;
        }
        s.source.Play();
    }

    private void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => name == sound.name);

        if (s == null)
        {
            Debug.LogWarning($"Missing sound {name}!");
            return;
        }

        s.source.Stop();
    }
}
