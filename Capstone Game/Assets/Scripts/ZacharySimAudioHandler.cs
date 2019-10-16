using System;
using UnityEditor;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public SourceForAudioHandler[] sounds;

    private void Awake()
    {
        foreach (SourceForAudioHandler source in sounds)
        {
            source.source = gameObject.AddComponent<AudioSource>();
            source.source.clip = source.audioClip;
            source.source.volume = source.volumeLevel;
            source.source.pitch = source.volumePitch;
            source.source.mute = source.mute;

        }
    }
    public void Start()
    {
        foreach (SourceForAudioHandler source in sounds)
        {
          
            if (source.source.clip == null)
            {
                Debug.LogError(source.source.clip + " Source clip is Null");
            }
            if (source.source.volume == 0f)
            {
                Debug.LogError(source.source.volume + " No Level Of Volume Specified");
            }
            if (source.source.pitch == 0f)
            {
                Debug.LogError(source.source.pitch + " No Level Of Pitch Given");
            }
            if (source.source.mute == false)
            {
                Debug.LogWarning(source.source.mute + " Has Been Muted By Default");
            }

        }
    }


    public void Play(string name)
    {
        SourceForAudioHandler source = Array.Find(sounds, sound => sound.audioName == name);
        source.source.Play();
    }
}
