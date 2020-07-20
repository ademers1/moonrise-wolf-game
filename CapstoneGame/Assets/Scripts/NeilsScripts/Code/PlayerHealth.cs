using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public float immune;
    public Slider slider;
    public Material primaryMat;
    public Material flashMat;

    
    public float flashLength;
    private float flashTimeRemaining;
    private bool flashActive;
    [SerializeField]
    


    public SkinnedMeshRenderer meshRenderer;
    Color originalColor;
    

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        //modelRender1 = GetComponent<MeshFilter>();
        //originalColor = meshRenderer.material.color;
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    public void EndGame()
    {
        //TODO end the game.
    }
   
    public void Flash()
    {
        flashTimeRemaining = flashLength;
        StartCoroutine(FlashRoutine());
    }

    

    // Update is called once per frame
    void Update()
    {
        if(flashTimeRemaining > 0)
        {
            flashTimeRemaining -= Time.deltaTime;// Subtract Timer to break out of Co routine
        }
    }

    public void Damage(int amount)
    {
        if(flashTimeRemaining > 0)
        {
            return;
        }
        
        if ((health -= amount) <= 0)
        {
            
            SceneManager.LoadScene(2);
        }
        else
        {
            Flash();

        }

        slider.value = (float)health / maxHealth;
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
