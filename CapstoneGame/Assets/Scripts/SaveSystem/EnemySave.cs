using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySave : MonoBehaviour
{
    NPCHealth enemy;
    

    int currentEnemiesLength;
    //find all the enemies in the scene, store in an array
    public GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    public int[] enemyInstanceIDs; 
    private void Start()
    {
        enemy = this.gameObject.GetComponent<NPCHealth>();
        currentEnemiesLength = enemies.Length;
        //initial assigning instance IDs
        for (int i = 0; i < enemies.Length; i++)
        {
            //match Instance IDs with proper gameObjects
            enemyInstanceIDs[i] = enemies[i].GetInstanceID();
        }
    }
    void Update()
    {
        //if amount of enemies changes
        if (enemies.Length != currentEnemiesLength)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                //re-assign Instance IDs with proper gameObjects
                enemyInstanceIDs[i] = enemies[i].GetInstanceID();
            }
        }
        //Call the Save System's Save Player function when you press 1. Pass it the current Player script component.
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Save();
        }

        //Call the Save System's Load Player function
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            //Load player returns type PlayerData
            EnemyData data = EnemySaveSystem.LoadEnemy();
            if (data != null)
            {
                enemy.Health = data.enemyHealth;
                transform.position = new Vector3(data.enemyPosition[0], data.enemyPosition[1], data.enemyPosition[2]);
            }
        }
    }
    public void Save()
    {
        EnemySaveSystem.SaveEnemy();
    }
    
    
}
