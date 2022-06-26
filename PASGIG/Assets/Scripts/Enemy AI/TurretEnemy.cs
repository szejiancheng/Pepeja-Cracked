using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
    public GameObject MissilePrefab;
    public Transform muzzle;

    float fireRate;
    float nextFire;
    // Start is called before the first frame update
    void Start()
    {
        fireRate = 5f;
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfTimeToFire();
    }

    void CheckIfTimeToFire()
    {
        if(Time.time > nextFire) 
        {
            Instantiate(MissilePrefab, muzzle.position, Quaternion.identity * Quaternion.Euler(0, 0, 90));
            nextFire = Time.time + fireRate;
        }
    }
}