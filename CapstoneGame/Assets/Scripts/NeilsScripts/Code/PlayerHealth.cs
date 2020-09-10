using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Killable
{
    float healthBarWidth = 125;
    float newWidth;
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

        if (Input.GetKeyDown(KeyCode.P))
        {
            Health -= 20;
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
            newWidth = healthBarWidth * ratio;
            healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(newWidth, healthBar.GetComponent<RectTransform>().sizeDelta.y);
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "MeleeWeapon")
        {
            Health -= 25;

        }
    }

}
