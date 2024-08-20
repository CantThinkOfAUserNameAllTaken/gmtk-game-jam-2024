using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Variables/AudioList")]
public class AudioList : ScriptableObject
{
    [SerializeField]
    private Sound[] sounds;


    public void PlaySound(string name, GameObject SoundSource)
    {
        Sound s = Array.Find(sounds, sound => name == sound.name);
        s.source = SoundSource.AddComponent<AudioSource>();
        if (s.source.loop == false)
        {
            Destroy(s.source, s.clips[0].length);
        }
            s.source.clip = s.clips[0];

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
        if (s.clipSelector != null)
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


