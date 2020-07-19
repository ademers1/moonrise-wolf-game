using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Data class that will be saved. It needs the System.Serializable attribute to be used by the Binary Formatter
[System.Serializable]
public class PlayerData
{
    public float[] playerPosition;
    public float playerHealth;

    //Constructor to create the default player data class
    public PlayerData(CharacterHealth player)
    {
        //get player health
        playerHealth = player.health;
        //get player position
        playerPosition = new float[3];
        playerPosition[0] = player.transform.position.x;
        playerPosition[1] = player.transform.position.y;
        playerPosition[2] = player.transform.position.z;
    }
}
