using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehaviour : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    // Update is called once per frame
    void Start ()
    {
        rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
        Debug.Log(rb.velocity.magnitude);
    }
}
