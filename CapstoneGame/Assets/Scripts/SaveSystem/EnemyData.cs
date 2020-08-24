﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Data class that will be saved. It needs the System.Serializable attribute to be used by the Binary Formatter
[System.Serializable]
public class EnemyData
{
    public float[] enemyPosition;
    public float enemyHealth;

    //Constructor to create the default player data class
    public EnemyData(NPCHealth enemy)
    {
        //get player health
        enemyHealth = enemy.enemyCurrentHealth;
        //get player position
        enemyPosition = new float[3];
        enemyPosition[0] = enemy.enemyCurrentPosition.x;
        enemyPosition[1] = enemy.enemyCurrentPosition.y;
        enemyPosition[2] = enemy.enemyCurrentPosition.z;
    }
}
