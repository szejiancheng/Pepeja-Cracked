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
    public PlayerSpawner playerSpawner; //assigned at instantiation


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

    public void DestroyPlayer()
    {
        playerSpawner.PlayerDestroyed();
    }
    
    // Update is called once per frame
    void Update()
    {
        //update physics
        //update 
        
    }
}
