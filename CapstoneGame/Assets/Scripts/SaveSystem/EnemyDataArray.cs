using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Data class that will be saved. It needs the System.Serializable attribute to be used by the Binary Formatter
[System.Serializable]
public class EnemyDataArray
{
    public EnemyData[] enemyDatas;

    public EnemyDataArray()
    {
        enemyDatas = new EnemyData[0];

    }

    public void Add(EnemyData e)
    {
        EnemyData[] temp = new EnemyData[enemyDatas.Length+1];
        enemyDatas.CopyTo(temp, 0);
        temp[enemyDatas.Length] = e;
        enemyDatas = temp;
    }

    public void Remove(EnemyData e)
    {
        
        for (int i = 0; i < enemyDatas.Length; i++)
        {

        }
    }
}
