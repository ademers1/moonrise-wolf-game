using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector]
    public CameraController Camera;

    //cache
    private AudioManager audioManager;
    private string ambienceAudioClip = "AmbientNoise";
    private string winAudioClip = "Victory";
    private string lossAudioClip = "Gameover";
    private enum MusicState { None, Ambience, Loss, Win }
    MusicState _musicState;

    void Awake()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        audioManager = AudioManager.instance;
    }

    public void GetCamera()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }

    private void Start()
    {
        
        if (audioManager == null)
        {
            Debug.Log("No Audiomanager found in scene");
        }
       
    }

    private void Update()
    {
        Instance.PlayGameMusic();
    }


    public void PlaySound(string soundName)
    {
        audioManager.PlaySound(soundName);
    }

    public void PlayMusic(string musicName)
    {
        audioManager.PlayMusic(musicName);
    }

    private void StopMusic(string musicName)
    {
        audioManager.StopMusic(musicName);
    }

    public void PlayGameMusic()
    {
        Debug.Log(SceneManager.GetActiveScene());
        if(SceneManager.GetSceneByName("Alex's Level") == SceneManager.GetActiveScene() && _musicState != MusicState.Ambience)
        {
            PlayMusic(ambienceAudioClip);
            _musicState = MusicState.Ambience;
            StopMusic(winAudioClip);
            StopMusic(lossAudioClip);
        }
        if(SceneManager.GetSceneByName("Loss") == SceneManager.GetActiveScene() && _musicState != MusicState.Loss)
        {
            PlayMusic(lossAudioClip);
            _musicState = MusicState.Loss;
            StopMusic(ambienceAudioClip);
            StopMusic(winAudioClip);
        }
        if(SceneManager.GetSceneByName("Win") == SceneManager.GetActiveScene() && _musicState != MusicState.Win)
        {
            PlayMusic(winAudioClip);           
            _musicState = MusicState.Win;
            StopMusic(ambienceAudioClip);
            StopMusic(lossAudioClip);
        }
    }

}


