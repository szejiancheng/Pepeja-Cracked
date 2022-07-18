using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{


    [SerializeField]
    private PlanePhysics planephysics;
    public Rigidbody2D rb;
    private PlayerHP playerhp;
    private PlayerCollision collision;
    private Joystick joystick;


    private void Awake() {
        //Initialize all components
    }

    // Start is called before the first frame update
    void Start()
    {
        //assign all necessary fields
        planephysics = gameObject.GetComponent<PlanePhysics>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerhp = gameObject.GetComponent<PlayerHP>();
        collision = gameObject.GetComponent<PlayerCollision>();
        joystick = (Joystick) GameObject.Find("Fixed Joystick").GetComponent<Joystick>();
        //transform.GetChild(1).gameObject.GetComponent<Joystick>(); //this is scary but whatever.
    }

    public float getJoyStickX()
    {
        return joystick.Horizontal;
    }

    public float getJoyStickY()
    {
        return joystick.Vertical;
    }
    public Vector2 getJoyStickDir()
    {
        return joystick.Direction;
    }
    
    // Update is called once per frame
    void Update()
    {
        //update physics
        //update 
        
    }
}
