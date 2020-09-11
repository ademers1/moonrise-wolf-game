using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySave : MonoBehaviour
{
    NPCHealth enemy;
    public EnemyData enemyData;

    int currentEnemiesLength;
    //find all the enemies in the scene, store in an array
    public GameObject[] enemies;
    public int[] enemyInstanceIDs; 
    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemy = this.gameObject.GetComponent<NPCHealth>();
        currentEnemiesLength = enemies.Length;
        enemyInstanceIDs = new int[enemies.Length];
        //initial assigning instance IDs
        for (int i = 0; i < enemies.Length; i++)
        {
            //match Instance IDs with proper gameObjects
            enemyInstanceIDs[i] = enemies[i].GetInstanceID();
        }
        //get instance ID
        enemyData.id = this.gameObject.GetInstanceID();
        //Add enemyData to the list
        SaveData.current.enemyDatas.Add(enemyData);
    }
    void Update()
    {
        /*
        enemyData.enemyPosition[0] = transform.position.x;
        enemyData.enemyPosition[1] = transform.position.y;
        enemyData.enemyPosition[2] = transform.position.z;
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
            //EnemyData data = EnemySaveSystem.LoadEnemy();
            for (int i = 0; i < SaveData.current.enemyDatas.Count; i++)
            {
                EnemyData currentData = SaveData.current.enemyDatas[i];
                //enemy gets maxHP
                enemy.Health = enemy.MaxHealth;
                transform.position = new Vector3(currentData.enemyPosition[0], currentData.enemyPosition[1], currentData.enemyPosition[2]);
            }

            //if (enemyData != null)
            //{
            //    //enemy gets maxHP
            //    enemy.Health = enemy.MaxHealth;
            //    transform.position = new Vector3(enemyData.enemyPosition[0], enemyData.enemyPosition[1], enemyData.enemyPosition[2]);
            //}
        }*/
    }
    public void Save()
    {
        EnemySaveSystem.SaveEnemy(enemyData);
    }
    
    
}
