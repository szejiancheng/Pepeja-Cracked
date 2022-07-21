using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
    public GameObject MissilePrefab;
    public Transform muzzle;

    float fireRate;
    float nextFire;
    
    public Transform target;

    // Start is called before the first frame update

    void Start()
    {
        fireRate = 5f;
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        CheckFire();
    }

    void CheckFire()
    {
        if (GameObject.FindWithTag("Player") != null) {
            if(Time.time > nextFire) 
            {
                SpawnMissile();
                nextFire = Time.time + fireRate;
            }
        }
    }

    void SpawnMissile()
    {
        GameObject missile = Instantiate(MissilePrefab, muzzle.position, Quaternion.identity * Quaternion.Euler(0, 0, 90));
        missile.GetComponent<MissileBehaviour>().speed = 500f;
    }
}
