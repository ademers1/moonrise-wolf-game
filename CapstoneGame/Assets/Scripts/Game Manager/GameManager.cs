using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : Singleton<GameManager>
{
    //cache
    private AudioManager audioManager;
    private string ambienceAudioClip = "AmbientNoise";
    private string winAudioClip = "Victory";
    private string lossAudioClip = "Gameover";
    private enum MusicState { None, Ambience, Loss, Win }
    MusicState _musicState;
    public List<GameObject> enemies = new List<GameObject>();

    void Awake()
    {
        audioManager = AudioManager.instance;
    }

    private void Start()
    {
        
        if (audioManager == null)
        {
            Debug.Log("No Audiomanager found in scene");
        }
        enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        
    }

    private void Update()
    {
        Instance.PlayGameMusic();
    }

    public void HideMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ShowMouse()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
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

    public void EnemyKilled()
    {

    }

    public void Died()
    {
        SceneManager.LoadScene("Loss");
    }

    public void PlayGameMusic()
    {
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


