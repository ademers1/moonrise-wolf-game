using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Killable
{
    public Image healthBar;

    public MusicControl musicSystem;                 //MusicControl Script on the "MusicSystem" GameObject

    [FMODUnity.EventRef]
    public string playerHurtSound = ""; //place event path in ""
    [FMODUnity.EventRef]
    public string playerDeathSound = ""; //place event path in ""

    FMODUnity.StudioEventEmitter emitter;


    public void EndGame()
    {
        //TODO end the game.
    }

    public void Flash()
    {
        flashTimeRemaining = flashLength;
        StartCoroutine(FlashRoutine());
    }

    private void Start()
    {
        MaxHealth = 100;
        Health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (flashTimeRemaining > 0)
        {
            flashTimeRemaining -= Time.deltaTime;// Subtract Timer to break out of Co routine
        }

    }

    public override float Health
    {
        get
        {
            return base.Health;
        }
        set
        {
            base.Health = value;
            float ratio = Health / MaxHealth;
            float newRatio = ratio * 0.78f;
            healthBar.fillAmount = newRatio;
        }
    }
    void Respawn()
    {
        //Respawn when damage hits 0
    }

    IEnumerator FlashRoutine()
    {
        while(flashTimeRemaining > 0)
        {
            if(flashActive)
            {
                flashActive = false;
                meshRenderer.material = primaryMat;

            }
            else
            {
                flashActive = true;
                meshRenderer.material = flashMat;
            }
            yield return new WaitForSeconds(.1f);

        }
        flashActive = false;
        meshRenderer.material = primaryMat;
        Invulnerable = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MeleeWeapon")
        {
            other.enabled = false;
            //TakeDamage();
            Health -= 25;
            StartCoroutine("FlashRoutine");
        }
    }


    
}
