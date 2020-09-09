using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyManager : MonoBehaviour
{
    public void OnSave()
    {
        SerializationManager.Save("enemySave", SaveData.current);
    }
    public void Onload()
    {
        SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/enemysave.save");

        for(int i=0; i<SaveData.current.enemyDatas.Count; i++)
        {
            EnemyData currentEnemy = SaveData.current.enemyDatas[i];
            //GameObject obj = Instantiate(enemyDatas[(int)currentEnemy.enemyType]);
            //EnemyDataHandler enemyDataHandler = obj.GetComponent<EnemyDataHandler>();
            //enemyDataHandler.enemyData = currentEnemy;
            //enemyDataHandler.transform.position = currentEnemy.position;
            //enemyDataHandler.transform.rotation = currentEnemy.rotation;
        }
    }
}
