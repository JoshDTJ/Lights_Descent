using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scri_GemSpawn : MonoBehaviour
{
    public GameObject[] spawnees;

    int randomInt;
    public void SpawnRandom()
    {
        randomInt = Random.Range(0, spawnees.Length);
        Instantiate(spawnees[randomInt], transform.position, Quaternion.identity);
    }
}
