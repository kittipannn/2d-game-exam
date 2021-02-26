

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject[] enemiesType;
    public GameObject[] spawnPoint;

    void Start()
    {
        for (int i = 0; i < enemiesType.Length; i++)
        {
            Instantiate(enemiesType[i], spawnPoint[i].transform.position, Quaternion.identity);
        }
    }

}
