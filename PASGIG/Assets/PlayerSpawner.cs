using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    public List<Transform> spawnPoints = new List<Transform>();
    public GameObject playerPrefab; 
    public GameObject player;

    private Transform nextSpawnPoint;
    //private static Random random = new Random();

    void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, getNextSpawnPoint().position, getNextSpawnPoint().rotation);
        player.GetComponentInChildren<PlanePhysics>().Speed = 500;
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
            if (player != null)
            {
                StartCoroutine(Respawn());
            } else {
                SpawnPlayer();
            }
        } 
    }

    IEnumerator waiter(int secondsToWait)
    {
        Debug.Log("Waiting for " + secondsToWait + " seconds.");
        yield return new WaitForSeconds(secondsToWait);
    }   

    IEnumerator Respawn()
    {
        Destroy(player);
        Debug.Log("Respawning ");
        yield return new WaitForSeconds(4);
        SpawnPlayer();
    }   
}
