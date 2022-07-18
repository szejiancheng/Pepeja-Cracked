using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePhysics : MonoBehaviour
{

    public PlayerMain playermain;
    public float Speed;
    public float Acceleration;
    public float RotationControl;
    

    //Joystick params
    float MovY, MovX = 1;
    Vector2 JoystickDir = Vector2.zero;
    float joystickMagnitude = 0;

    //Wing behavior
    public float wingDrag = 1f; //this is for how much you slow down when you turn and how much of
    //this force gets converted into forward movement, like wingsize
    public float efficiency = 0.7f;

    //Directional change vars
    bool leftFacing;


    // Start is called before the first frame update
    void Start()
    {
        playermain = GetComponent<PlayerMain>();
        leftFacing = (Vector2.Dot(-transform.right, Vector2.right) < 0);
        Debug.Log("Leftfacing isTrue: " + leftFacing);
        if (!leftFacing){
            Flip();
        }

    }

    // Update is called once per frame
    
    void Update()
    {
        MovY = playermain.getJoyStickX();
        MovX = playermain.getJoyStickY();
        JoystickDir = playermain.getJoyStickDir();
    }
    
    private void FixedUpdate()
    {


        joystickMagnitude = (float) Math.Sqrt(MovY*MovY + MovX*MovX);

        playermain.rb.AddForce(CalculateThrust() + CalculateWingPhysics());
        //adding thrust based on magnitude of joystick displacement + wing drag + forward translated movement: depends on angle hitting wing
        //turning, speed based on rotation control
        pitch();

        
        //speed limit enforced using Speed
        if(playermain.rb.velocity.magnitude > Speed)
        {
            playermain.rb.velocity = playermain.rb.velocity.normalized * Speed;
        }
        

        //adding nose droop like when control surfaces are disabled
        
        if(MovX == 0 && MovY == 0)
        {
            float Dir = Vector2.SignedAngle(playermain.rb.velocity, -transform.right);
            playermain.rb.rotation -= Dir * RotationControl;
        }
        
        
        
        //adding check to flip X scale of playerobject
        if(playermain.rb.velocity.x > 0)
        {
            if (leftFacing)
            {
                leftFacing = false;
                Flip();
            }
            
        } else {
            if (!leftFacing)
            {
                leftFacing = true;
                Flip();
            }
        }
        
        
    }
    private void Flip()
	{
        //TODO: trigger to activate animation
		//transform.Rotate(180f, 0f, 0f);
        //transform.GetChild(1).Rotate(180f, 0f, 0f);
        gameObject.GetComponent<SpriteRenderer>().flipY = !gameObject.GetComponent<SpriteRenderer>().flipY;
	}

    private Vector2 CalculateThrust() 
    {
        Vector2 thrust = -transform.right * (joystickMagnitude * Acceleration);
        Debug.DrawLine(transform.position, (Vector2) transform.position + thrust, Color.yellow, 0.0f);
        return thrust;
    }

    void pitch()
    {
        if(Acceleration > 0)
        {
            
            //transform.left will be fixed as the forward direction
            float Dir = Vector2.SignedAngle(JoystickDir, -transform.right);
            playermain.rb.rotation -= Dir * RotationControl * Time.deltaTime;
            


        }
    }
    Vector2 CalculateWingPhysics()
    {
        //component perpendicular to wing
        Vector2 velocity = playermain.rb.velocity;
        Vector2 up = transform.up;
        float dot = Vector2.Dot(up, velocity);
        Vector2 drag = up * dot * -1.0f * wingDrag;
        Debug.DrawLine(transform.position, (Vector2) transform.position + drag, Color.green, 0.0f);

        //component parallel to wing
        float angleOfAttack = Vector2.Angle(-transform.right, playermain.rb.velocity);
        Vector2 forwardMomentum = drag.magnitude * transform.right * efficiency * (1.0f - (angleOfAttack/90));
        Debug.DrawLine(transform.position, (Vector2) transform.position + forwardMomentum, Color.red, 0.0f);

        return drag + forwardMomentum;
    }
}
