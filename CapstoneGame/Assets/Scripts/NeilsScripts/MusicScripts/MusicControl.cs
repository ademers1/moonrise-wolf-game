using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{

    [FMODUnity.EventRefAttribute]
    public string music = "event:/Music/Music";
    FMOD.Studio.EventInstance musicEv;

    

    // Start is called before the first frame update
    void Start()
    {
        musicEv = FMODUnity.RuntimeManager.CreateInstance(music);
        musicEv.start();
        
    }

    //Player has selected play Game from menu 
    public void GameStartedMusic()
    {
        musicEv.setParameterByName("GameStarted", 1f);
    }

    //Player is less than 25% health 
    public void isUnderHealthAmount()
    {
        musicEv.setParameterByName("IsUnderHealthAmount", 1f);
    }

    //Player is dead
    public void IsDeadMusic()
    {
        musicEv.setParameterByName("IsDead", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
