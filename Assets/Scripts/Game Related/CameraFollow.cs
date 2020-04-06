using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;


     void Awake()
     {
         //camera distance 
        // GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / cameraDistance);
     }

    // Update is called once per frame
    void Update()
    {
        //main camera follows the player
        transform.position = new Vector3(player.position.x,player.position.y,transform.position.z);
    }
    
    
}

