using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Data class that will be saved. It needs the System.Serializable attribute to be used by the Binary Formatter
[System.Serializable]
public class EnemyDatas
{
    public EnemyData[] enemyDatasArray;

    //Constructor to create the default player data class
    public EnemyDatas(EnemyData[] enemyDatas)
    {
        this.enemyDatasArray = enemyDatas;
    }

}
