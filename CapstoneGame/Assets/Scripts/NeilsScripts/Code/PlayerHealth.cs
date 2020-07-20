using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Killable
{
    

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
    }

    // Update is called once per frame
    void Update()
    {
        if(flashTimeRemaining > 0)
        {
            flashTimeRemaining -= Time.deltaTime;// Subtract Timer to break out of Co routine
        }
    }

    public void Damage(float amount)
    {
        Health -= (int)amount;

        slider.value = (float)Health / (float)MaxHealth;
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
    
}
