using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    public float Speed;
    public float Acceleration;
    public float RotationControl;
    public Joystick joystick;

    Rigidbody2D rb;

    //Joystick params
    float MovY, MovX = 1;
    Vector2 JoystickDir = Vector2.zero;

    //Wing behavior
    public float wingDrag = 1f; //this is for how much you slow down when you turn and how much of
    //this force gets converted into forward movement, like wingsize
    public float wingLift = 0.5f; //this for when you don't want your plane to crash (I'd rename it)
    //crashability but like that's ultimately unhelpful

    //Directional change vars
    bool rightFacing = true;
    float timeSinceDirChange = 0;


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
        float joystickMagnitude = (float) Math.Sqrt(MovX*MovX + MovY*MovY);
        Vector2 Vel = transform.right * (joystickMagnitude * Acceleration);
        rb.AddForce(Vel);

        
        if(Acceleration > 0)
        {
            //transform.right will be fixed as the forward direction
            float Dir = Vector2.SignedAngle(JoystickDir, transform.right);
            rb.rotation -= Dir * RotationControl;
        }
        
        if(rb.velocity.magnitude > Speed)
        {
            rb.velocity = rb.velocity.normalized * Speed;
        }

        //adding wing drag
        Vector2 bleed = wingDrag * transform.up.normalized * Vector2.Dot(transform.up, rb.velocity);
        rb.AddForce(bleed * -1.0f);
        rb.AddForce(bleed.magnitude * transform.right);

        //adding nose droop like when control surfaces are disabled
        if(MovX == 0 && MovY == 0)
        {
            float Dir = Vector2.SignedAngle(rb.velocity, transform.right);
            rb.rotation -= Dir * RotationControl;
        }

        //adding wing lift
        rb.AddForce(rb.velocity.x * transform.up * wingLift);
        /*
        float horizontalVelocity = Vector2.Dot(rb.velocity, Vector2.right);
        rb.AddForce(horizontalVelocity * Vector2.up * wingLift);
        */

        //adding check to flip y scale of playerobject
        //
        if(rb.velocity.x < 0)
        {
            if (rightFacing)
            {
                rightFacing = false;
                Flip();
            }
        } else 
        {
            if (!rightFacing)
            {
                rightFacing = true;
                Flip();
            }
        }
    }
    private void Flip()
	{
        //TODO: trigger to activate animation
		transform.Rotate(180f, 0f, 0f);
	}
}