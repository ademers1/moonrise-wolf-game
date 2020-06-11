using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0f, 0.5f)]
    public float randomVolume = 0.1f;
    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.Play();
    }

    public void PlayMusic()
    {
        Debug.Log(source);
        
        //source.volume = volume;
        source.pitch = pitch;
        source.loop = true;
        source.Play();
    }

    public void StopMusic()
    {
        source.Stop();
    }
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    Sound[] sounds;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one AudioManager in the Scene.");
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }

        foreach (Sound sound in sounds)
        {
            GameObject _go = new GameObject("Sound_" + System.Array.IndexOf(sounds, sound) + "_" + sound.name);
            _go.transform.SetParent(this.transform);
            sound.SetSource(_go.AddComponent<AudioSource>());
        }
    }

    private void Start()
    {

    }

    public void PlaySound(string _name)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name == _name)
            {
                sound.Play();
                return;
            }
        }
        // no sound with name
        Debug.LogWarning("Audio Manager: Sound not found in sounds list: " + _name);
    }

    public void PlayMusic(string _name)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name == _name)
            {
                sound.PlayMusic();
                return;
            }
        }
        // no sound with name
        Debug.LogWarning("Audio Manager: Sound not found in sounds list: " + _name);
    }

    public void StopMusic(string _name)
    {
        foreach (Sound sound in sounds)
        {
            if(sound.name == _name)
            {
                sound.StopMusic();
                return;
            }
        }
    }
}
