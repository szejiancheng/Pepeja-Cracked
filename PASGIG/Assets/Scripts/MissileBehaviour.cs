using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehaviour : MonoBehaviour
{
    public Transform player;
    public float speed = 40f;
    public Rigidbody2D rb;
    public float maxLifetime = 2.0f;
    private Vector2 movement;
    public float turnSpeed = 0.5f;
    public float directionFacing;
    
    void Start ()
    {
        rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0) Explode();

        if(player != null)
        {
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            directionFacing = angle * turnSpeed;
            
        }
    }

    private void Explode () 
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("An enemy hit you!");
        
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("An enemy hit you!");
            Explode();
        }
        Explode();
    }

    private void FixedUpdate() 
    {
        moveMissile(directionFacing);
    }
    /*
    void moveMissile(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }
    */
    void moveMissile(float angle)
    {
        rb.rotation = directionFacing;
        rb.AddForce(transform.right * speed);
    }
}
