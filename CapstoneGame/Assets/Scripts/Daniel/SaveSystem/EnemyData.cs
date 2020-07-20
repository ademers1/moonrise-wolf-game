using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Data class that will be saved. It needs the System.Serializable attribute to be used by the Binary Formatter
[System.Serializable]
public class EnemyData
{
    public float[] enemyPosition;
    public float[] enemyRotation;
    public float[] enemyScale;
    public float enemyHealth;

    //Constructor to create the default player data class
    public EnemyData(EnemyHealth enemy)
    {
        //get player health
        enemyHealth = enemy.health;
        //get player position
        enemyPosition = new float[3];
        enemyPosition[0] = enemy.transform.position.x;
        enemyPosition[1] = enemy.transform.position.y;
        enemyPosition[2] = enemy.transform.position.z;
        //get player rotation
        enemyRotation = new float[4];
        enemyRotation[0] = enemy.transform.rotation.x;
        enemyRotation[1] = enemy.transform.rotation.y;
        enemyRotation[2] = enemy.transform.rotation.z;
        enemyRotation[3] = enemy.transform.rotation.w;
        //get player scale
        enemyScale = new float[3];
        enemyScale[0] = enemy.transform.localScale.x;
        enemyScale[1] = enemy.transform.localScale.y;
        enemyScale[2] = enemy.transform.localScale.z;
    }
}
