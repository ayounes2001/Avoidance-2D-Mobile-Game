using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    #region Spawning Enemies
//spawning Enemies
    public GameObject enemy;
    public Collider2D[] colliders;
    public float radius;
    private int safetyNet = 0;
    public LayerMask layerMask;

    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;

     void Start()
    {
        InvokeRepeating("SpawnEnemy",spawnTime,spawnTime);
    }

    public void SpawnEnemy()
    {
        Vector3 spawnPos = new Vector3(0,0,0);
        bool canSpawnHere = false;
        
        //Random spawn between a certain x and y radius
      
        while (!canSpawnHere)
        {
            float spawnPosX = Random.Range(-25f, 23f);
            float spawnPosY = Random.Range(-10f, 17f);
            
            spawnPos = new Vector3(spawnPosX,spawnPosY,0);
            canSpawnHere = PreventSpawnOverlap(spawnPos);
            if (canSpawnHere)
            {
                break;
            }

            safetyNet++;
            if (safetyNet > 50)
            {
                break;
                Debug.Log("Too many attempts");
            }
            
        }
        
        //Instatiate the object within that spawnRadius
        GameObject newEnemy = Instantiate(enemy,spawnPos,Quaternion.identity) as GameObject;
        if (stopSpawning)
        {
            CancelInvoke("SpawnEnemy");
        }
        
    }

    bool PreventSpawnOverlap (Vector3 spawnPos)
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            //check the bounds of each of the colliders and check if you can spawn there
            Vector3 centerPoint = colliders[i].bounds.center;
            float width = colliders[i].bounds.extents.x;
            float height = colliders[i].bounds.extents.y;

            float leftExtent = centerPoint.x - width;
            float rightExtent = centerPoint.x + width;
            float lowerExtent = centerPoint.y - height;
            float upperExtent = centerPoint.y + height;
//if the enemy cannot spawn within those bounds then return false
            if (spawnPos.x >= leftExtent && spawnPos.x <= leftExtent)
            {
                if (spawnPos.y >= lowerExtent && spawnPos.y <= upperExtent)
                {
                    return false;
                }
            }

            
        }
        return true;
    }

    #endregion
    
}
