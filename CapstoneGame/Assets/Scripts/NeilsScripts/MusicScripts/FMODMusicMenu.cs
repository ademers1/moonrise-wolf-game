using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODMusicMenu : MonoBehaviour
{

    public static FMOD.Studio.EventInstance MenuMusic;
    // Start is called before the first frame update
    void Start()
    {
        MenuMusic = FMODUnity.RuntimeManager.CreateInstance("event:/MenuMusic");
        MenuMusic.start();
        MenuMusic.release();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
