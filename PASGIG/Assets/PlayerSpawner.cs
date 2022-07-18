using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    public List<Transform> spawnPoints = new List<Transform>();
    public GameObject playerPrefab; 

    private Transform nextSpawnPoint;
    //private static Random random = new Random();

    void SpawnPlayer()
    {
        Instantiate(playerPrefab, getNextSpawnPoint().position, getNextSpawnPoint().rotation);
        nextSpawnPoint = null;
    }

    public Transform getNextSpawnPoint()
    {
        if (nextSpawnPoint == null)
        {
            nextSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        }
        return nextSpawnPoint;
    }
    void Update()
    {
        /*** FOR DEBUG ***/
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnPlayer();
        }
    }
}
