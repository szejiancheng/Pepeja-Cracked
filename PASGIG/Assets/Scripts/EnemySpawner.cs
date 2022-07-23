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

    public float MaxX = 280f;
    public float MinX = -90f;
    public float MaxY = 50f;
    public float MinY = -30f;
    public float FloorY = -40f;

    public Transform playerTransform;
 


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
            playerTransform = GameObject.FindWithTag("Player Object").transform;
        }    
    }

    private IEnumerator spawnFlyingEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector2(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY)), Quaternion.identity);
        newEnemy.GetComponent<FlyingEnemy>().spawner = GetComponent<EnemySpawner>();
        StartCoroutine(spawnFlyingEnemy(interval, enemy));
    }

    private IEnumerator spawnGroundEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector2(Random.Range(MinX, MaxX), Random.Range(FloorY-5, FloorY)), Quaternion.identity);
        newEnemy.GetComponent<TurretEnemy>().spawner = GetComponent<EnemySpawner>();
        StartCoroutine(spawnGroundEnemy(interval, enemy));
    }
}