using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum EnemyType
{
    Melee,
    Ranged
}
//Data class that will be saved. It needs the System.Serializable attribute to be used by the Binary Formatter
[System.Serializable]
public class EnemyData
{
    //public string id;
    public int id;
    public EnemyType enemyType;
    public Vector3 Position;
    public Quaternion rotation;
    public float enemyHealth;
    NPCHealth enemy;
    public float[] enemyPosition;

    //Constructor to create the default player data class
    public EnemyData()
    {
        //get player health
        //enemyHealth = enemy.MaxHealth;
        //get player position
        //enemyPosition = new float[3];
        //enemyPosition[0] = enemy.enemyCurrentPosition.x;
        //enemyPosition[1] = enemy.enemyCurrentPosition.y;
        //enemyPosition[2] = enemy.enemyCurrentPosition.z;
    }

}
