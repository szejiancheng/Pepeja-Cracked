using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    public float Speed;
    public float Acceleration;

    public Joystick joystick;

    Rigidbody2D rb;

    public float RotationControl;

    float MovY, MovX = 1;
    Vector2 JoystickDir = Vector2.zero;
    float wingDrag = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovY = joystick.Vertical;
        MovX = joystick.Horizontal;
        JoystickDir = joystick.Direction;

    }
    
    private void FixedUpdate()
    {
        /* 
        //x axis controls thrust
        Vector2 Vel = transform.right * (MovX * Acceleration);
        rb.AddForce(Vel);

        float Dir = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.right));

        if(Acceleration > 0)
        {
            if(Dir > 0)
            {
                rb.rotation += MovY * RotationControl *(rb.velocity.magnitude / Speed);
            }
            else
            {
                rb.rotation -= MovY * RotationControl *(rb.velocity.magnitude / Speed);
            }
        }

        float thrustForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.down)) * 2.0f;

        Vector2 relForce = Vector2.up * thrustForce;

        rb.AddForce(rb.GetRelativeVector(relForce));


        if(rb.velocity.magnitude > Speed)
        {
            rb.velocity = rb.velocity.normalized * Speed;
        } */


        //feels like the proper implementation is:
        //magnitude controls thrust
        //turn object towards desired vector
        float joystickMagnitude = (float) Math.Sqrt(MovX*MovX + MovY*MovY);
        Vector2 Vel = transform.right * (joystickMagnitude * Acceleration);
        rb.AddForce(Vel);

        
        if(Acceleration > 0)
        {
            float Dir = Vector2.SignedAngle(JoystickDir, transform.right);
            rb.rotation -= Dir * RotationControl;
        }
        
        if(rb.velocity.magnitude > Speed)
        {
            rb.velocity = rb.velocity.normalized * Speed;
        }

        //adding wing drag
        rb.AddForce(wingDrag * transform.up.normalized * Vector2.Dot(transform.up, rb.velocity) * -1.0f);
        //rb.velocity -= rb.velocity * wingDrag * (transform.up + transform.right) * 5.0f;

        //adding nose droop
        if(MovX == 0 && MovY == 0)
        {
            float Dir = Vector2.SignedAngle(rb.velocity, transform.right);
            rb.rotation -= Dir * RotationControl * 8;
        }
        //TODO: Properly implement wing simulation
    
    }
}