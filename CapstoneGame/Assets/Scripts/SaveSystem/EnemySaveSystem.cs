using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class EnemySaveSystem
{
    public static List<EnemyData> enemies;

    public static void SaveEnemy()
    {
        //Create Binary Formatter to write to a file
        BinaryFormatter formatter = new BinaryFormatter();

        //Path to our file creation - Unity chooses the filepath based on operating system
        string path = Application.persistentDataPath + "/enemydata.io";

        //Creating our stream to save the file
        FileStream stream = new FileStream(path, FileMode.Create);

        //PlayerData is now being created
        EnemyData data = new EnemyData();

        //Serializing the PlayerData
        formatter.Serialize(stream, data);

        //Closing the stream - VERY IMPORTANT STEP
        stream.Close();
    }

    //public static void SaveEnemies(List<GameObject> enemyList)
    //{
    //    enemies = new List<EnemyData>();
    //    foreach (GameObject g in enemyList)
    //    {
    //        enemies.Add(new EnemyData(g.GetComponent<NPCHealth>()));
    //    }

    //}

    public static EnemyData LoadEnemy()
    {
        //Path to our file - Unity chooses the filepath based on operating system
        string path = Application.persistentDataPath + "/enemydata.io";

        //Check to make sure if the file exists
        if (File.Exists(path))
        {
            //Create Binary Formatter to write to a file
            BinaryFormatter formatter = new BinaryFormatter();

            //Creating our stream to open the file
            FileStream stream = new FileStream(path, FileMode.Open);

            //Deserializing our data - Deserialize function ouputs an object so we have to cast the object to our PlayerData class
            EnemyData data = formatter.Deserialize(stream) as EnemyData;

            //Closing the stream - VERY IMPORTANT STEP
            stream.Close();

            //Return our PlayerData
            return data;
        }

        //if file doesn't exist, output an error.
        else
        {
            Debug.LogError("File does not exist");
            return null;
        }
    }
}
