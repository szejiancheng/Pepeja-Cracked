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
 


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnFlyingEnemy(kamikazeInterval, kamikazePrefab));
        StartCoroutine(spawnGroundEnemy(turretInterval, turretPrefab));
    }

    private IEnumerator spawnFlyingEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector2(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY)), Quaternion.identity);
        StartCoroutine(spawnFlyingEnemy(interval, enemy));
    }

    private IEnumerator spawnGroundEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector2(Random.Range(MinX, MaxX), Random.Range(FloorY-5, FloorY)), Quaternion.identity);
        StartCoroutine(spawnGroundEnemy(interval, enemy));
    }
}