using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{

    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] GameObject[] enemyList;
    [SerializeField] float interval;
    [SerializeField] float offsetRange;
    float randomOffsetX;
    float randomOffsetZ;
    float step;
    int enemyCount;

    void FixedUpdate()
    {
        //Get current amount of enemies loaded in
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        //Check if it's time to spawn enemies and if there is space for them
        if (step == interval && enemyCount < 100)
        {
            //Spawn for every spawnpoint
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                //Calculate random offset for spawn
                randomOffsetX = Random.Range(-offsetRange, offsetRange);
                randomOffsetZ = Random.Range(-offsetRange, offsetRange);
                //Spawn a random enemy from enemyList
                Instantiate(enemyList[Random.Range(0,enemyList.Length)], spawnPoints[i].transform.position + new Vector3(randomOffsetX, 0, randomOffsetZ), Quaternion.identity);
                //Reset spawn timer
                step = 0;
            }
        }
        //Increase spawn timer
        step++;
    }
}
