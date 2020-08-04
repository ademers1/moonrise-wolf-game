using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(PlayerHealth player)
    {
        //Create Binary Formatter to write to a file
        BinaryFormatter formatter = new BinaryFormatter();

        //Path to our file creation - Unity chooses the filepath based on operating system
        string path = Application.persistentDataPath + "/playerdata.io";

        //Creating our stream to save the file
        FileStream stream = new FileStream(path, FileMode.Create);

        //PlayerData is now being created
        PlayerData data = new PlayerData(player);

        //Serializing the PlayerData
        formatter.Serialize(stream, data);

        //Closing the stream - VERY IMPORTANT STEP
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        //Path to our file - Unity chooses the filepath based on operating system
        string path = Application.persistentDataPath + "/playerdata.io";

        //Check to make sure if the file exists
        if (File.Exists(path))
        {
            //Create Binary Formatter to write to a file
            BinaryFormatter formatter = new BinaryFormatter();

            //Creating our stream to open the file
            FileStream stream = new FileStream(path, FileMode.Open);

            //Deserializing our data - Deserialize function ouputs an object so we have to cast the object to our PlayerData class
            PlayerData data = formatter.Deserialize(stream) as PlayerData;

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
