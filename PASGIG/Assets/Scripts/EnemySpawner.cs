using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject kamikazePrefab;
    [SerializeField]
    private GameObject turretPrefab;

    [SerializeField]
    private float kamikazeInterval = 3.5f;
    [SerializeField]
    private float turretInterval = 10f;

    [SerializeField]
    private Transform leftBottomCorner;
    [SerializeField]
    private Transform rightTopCorner;

    public float MaxX = 280f;
    public float MinX = -90f;
    public float MaxY = 50f;
    public float MinY = -30f;

    

    public float FloorY = -40f;

    public Transform playerTransform;
    public GameObject spawnEffect;
 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnFlyingEnemy(kamikazeInterval, kamikazePrefab));
        StartCoroutine(spawnGroundEnemy(turretInterval, turretPrefab));
    }

    private void Update() 
    {
        if (playerTransform == null)
        {
            //try to find player
            if (GameObject.FindWithTag("Player Object") != null)
            {
                playerTransform = GameObject.FindWithTag("Player Object").transform;
            }
            
        }    
    }

    private IEnumerator spawnFlyingEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        Vector2 nextSpawnPos = new Vector2(Random.Range(leftBottomCorner.position.x, rightTopCorner.position.x), 
                                                                Random.Range(leftBottomCorner.position.y, rightTopCorner.position.y));
        GameObject effect = Instantiate(spawnEffect, nextSpawnPos, Quaternion.identity);

        GameObject newEnemy = Instantiate(enemy, nextSpawnPos, Quaternion.identity);
        FlyingEnemy EnemyAI = newEnemy.GetComponent<FlyingEnemy>();
        EnemyAI.spawner = GetComponent<EnemySpawner>();
        EnemyAI.moveSpeed = UnityEngine.Random.Range(5f, 75f);
        StartCoroutine(spawnFlyingEnemy(interval, enemy));
    }

    private IEnumerator spawnGroundEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector2(Random.Range(leftBottomCorner.position.x, rightTopCorner.position.x), leftBottomCorner.position.y), Quaternion.identity);
        newEnemy.GetComponent<TurretEnemy>().spawner = GetComponent<EnemySpawner>();
        StartCoroutine(spawnGroundEnemy(interval, enemy));
    }
}