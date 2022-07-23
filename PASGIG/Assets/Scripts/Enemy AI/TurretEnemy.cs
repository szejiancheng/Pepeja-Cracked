using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
    public GameObject MissilePrefab;
    public Transform muzzle;
    public EnemySpawner spawner;
    float fireInterval;
    float nextFire;
    
    public Transform target;

    // Start is called before the first frame update

    void Start()
    {
        fireInterval = 5f;
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        CheckFire();
    }

    void CheckFire()
    {
        if (target != null) {
            if(Time.time > nextFire) 
            {
                FireMissile();
                nextFire = Time.time + fireInterval;
            }
        } else 
        {
            target = spawner.playerTransform;
        }
    }

    void FireMissile()
    {
        GameObject missile = Instantiate(MissilePrefab, muzzle.position, Quaternion.identity * Quaternion.Euler(0, 0, 90));
        missile.GetComponent<MissileBehaviour>().speed = 500f;
    }
}
