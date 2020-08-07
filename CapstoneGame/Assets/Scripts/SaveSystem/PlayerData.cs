using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Data class that will be saved. It needs the System.Serializable attribute to be used by the Binary Formatter
[System.Serializable]
public class PlayerData
{
    public float[] playerPosition;
    public float[] playerRotation;
    public float[] playerScale;
    public int playerHealth;

    //Constructor to create the default player data class
    public PlayerData(PlayerHealth player)
    {
        //get player health
        playerHealth = player.Health;
        //get player position
        playerPosition = new float[3];
        playerPosition[0] = player.transform.position.x;
        playerPosition[1] = player.transform.position.y;
        playerPosition[2] = player.transform.position.z;
        //get player rotation
        playerRotation = new float[4];
        playerRotation[0] = player.transform.rotation.x;
        playerRotation[1] = player.transform.rotation.y;
        playerRotation[2] = player.transform.rotation.z;
        playerRotation[3] = player.transform.rotation.w;
        //get player scale
        playerScale = new float[3];
        playerScale[0] = player.transform.localScale.x;
        playerScale[1] = player.transform.localScale.y;
        playerScale[2] = player.transform.localScale.z;
    }
}
