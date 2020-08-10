using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunParticals : MonoBehaviour
{
    public ParticleSystem stunParticalLauncher;
    //public ParticleSystem rootParticalLauncher;
    //public ParticleSystem slowParticalLauncher;
    //public ParticleSystem zapParticalLauncher;
    public void startEmit(ParticleSystem launcher)
    {
        launcher.Play();
    }
    public void endEmit(ParticleSystem launcher)
    {
        launcher.Stop();
    }
}
