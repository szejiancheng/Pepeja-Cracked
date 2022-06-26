using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempWeapon : MonoBehaviour
{
    public Transform muzzle;
    public GameObject bulletPrefab;
    public Rigidbody2D playerRb;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject currentBullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        
        currentBullet.GetComponent<Rigidbody2D>().AddForce(playerRb.velocity, ForceMode2D.Impulse);
        //recoil
        //playerRb.AddForce()
    }
}
