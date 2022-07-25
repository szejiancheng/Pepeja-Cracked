using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehaviour : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public float maxLifetime = 2.0f;
    // Update is called once per frame
    void Start ()
    {
        rb.AddForce(-transform.right * speed, ForceMode2D.Impulse);
    }

    private void Update()
    {
        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0) Explode();
    }

    private void Explode () 
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("We hit something!");
        //Debug.Log(collision.gameObject.tag);
        /*
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Explode();
        }
        */

        Explode();
    }
}
