using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    public List<Transform> spawnPoints = new List<Transform>();
    public GameObject playerPrefab; 
    public GameObject beamPrefab; 
    public GameObject spawnExplosionPrefab; 
    public GameObject destExplosionPrefab;

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

    public void PlayerDestroyed()
    {
        StartCoroutine(DestroyPlayer());
    }

    IEnumerator Respawn()
    {
        Destroy(player);
        Debug.Log("Respawning ");
        yield return new WaitForSeconds(4);
        StartCoroutine(SpawnPlayer());
    }   

    public IEnumerator SpawnPlayer()
    {
        player = Instantiate(playerPrefab, getNextSpawnPoint().position, getNextSpawnPoint().rotation);
        player.GetComponentInChildren<PlanePhysics>().Speed = playerSpeed;
        player.GetComponentInChildren<PlayerMain>().playerSpawner = GetComponent<PlayerSpawner>();

        //Freeze player
        Rigidbody2D playerRb = player.GetComponentInChildren<Rigidbody2D>();
        playerRb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        SpriteRenderer sprite = player.GetComponentInChildren<SpriteRenderer>();
        sprite.enabled = false;
        Collider2D collider = player.GetComponentInChildren<Collider2D>();
        yield return new WaitForSeconds(1);
        collider.enabled = false;
        
        

        GameObject beam = Instantiate(beamPrefab, nextSpawnPoint.position, nextSpawnPoint.rotation);
        yield return new WaitForSeconds(2);
        Destroy(beam);



        playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        sprite.enabled = true;
        collider.enabled = true;
        playerRb.AddForce(-playerRb.transform.right * spawnLaunch);

        GameObject spawnExplosion = Instantiate(spawnExplosionPrefab, nextSpawnPoint.position, nextSpawnPoint.rotation);
        yield return new WaitForSeconds(2);
        Destroy(spawnExplosion);

        nextSpawnPoint = null;
    }

    public IEnumerator DestroyPlayer()
    {
        Debug.Log("Destroying Player");
        Debug.Log("spawning explosion");
        GameObject DestructExplosion = Instantiate(destExplosionPrefab, player.transform.Find("Player Object").position, player.transform.Find("Player Object").rotation);
        Destroy(player);
        yield return new WaitForSeconds(2);
        Destroy(DestructExplosion);
        Debug.Log("destroying explosion");

        GameMasterScript.GetInstance().RespawnPlayer();
    }
}
