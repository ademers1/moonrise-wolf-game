using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySave : MonoBehaviour
{
    EnemyHealth enemy;
    private void Start()
    {
        enemy = this.gameObject.GetComponent<EnemyHealth>();
    }
    void Update()
    {

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
                enemy.health = data.enemyHealth;
                transform.position = new Vector3(data.enemyPosition[0], data.enemyPosition[1], data.enemyPosition[2]);
            }
        }
    }
    public void Save()
    {
        EnemySaveSystem.SaveEnemy(enemy);
    }
    
    
}
