using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    public List<Transform> spawnPoints = new List<Transform>();
    public GameObject playerPrefab; 
    public GameObject beamPrefab; 
    public GameObject spawnExplosionPrefab; 
    public GameObject player;
    public float playerSpeed;
    public float spawnLaunch;

    private Transform nextSpawnPoint;
    //private static Random random = new Random();
    /***
    void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, getNextSpawnPoint().position, getNextSpawnPoint().rotation);
        player.GetComponentInChildren<PlanePhysics>().Speed = 500;
        nextSpawnPoint = null;
    }
    ***/

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
                StartCoroutine(SpawnPlayer());
            }
        } 
    }

    IEnumerator Respawn()
    {
        Destroy(player);
        Debug.Log("Respawning ");
        yield return new WaitForSeconds(4);
        StartCoroutine(SpawnPlayer());
    }   

    IEnumerator SpawnPlayer()
    {
        player = Instantiate(playerPrefab, getNextSpawnPoint().position, getNextSpawnPoint().rotation);
        player.GetComponentInChildren<PlanePhysics>().Speed = playerSpeed;

        //Freeze player
        Rigidbody2D playerRb = player.GetComponentInChildren<Rigidbody2D>();
        playerRb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        SpriteRenderer sprite = player.GetComponentInChildren<SpriteRenderer>();
        sprite.enabled = false;
        
        

        GameObject beam = Instantiate(beamPrefab, nextSpawnPoint.position, nextSpawnPoint.rotation);
        yield return new WaitForSeconds(2);
        Destroy(beam);



        playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        sprite.enabled = true;
        playerRb.AddForce(-playerRb.transform.right * spawnLaunch);

        GameObject spawnExplosion = Instantiate(spawnExplosionPrefab, nextSpawnPoint.position, nextSpawnPoint.rotation);
        yield return new WaitForSeconds(2);

        nextSpawnPoint = null;
    }
}
