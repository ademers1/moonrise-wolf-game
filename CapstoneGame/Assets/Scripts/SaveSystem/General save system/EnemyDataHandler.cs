using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDataHandler : MonoBehaviour
{
    public EnemyType enemyType;
    public EnemyData enemyData;
    // Start is called before the first frame update
    void Start()
    {
        //if(string.IsNullOrEmpty(enemyData.id))
        //{
        //    enemyData.id = System.DateTime.Now.ToLongDateString() + System.DateTime.Now.ToLongTimeString() + Random.Range(0, int.MaxValue).ToString();
        //    enemyData.enemyType = enemyType;
        //    SaveData.current.enemyDatas.Add(enemyData);
        //}


    }

    // Update is called once per frame
    void Update()
    {
        enemyData.Position = transform.position;
        enemyData.rotation = transform.rotation;
    }
}
