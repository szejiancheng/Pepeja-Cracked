using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    //TODO: optimize

    //https://www.youtube.com/watch?v=aLpixrPvlB8&ab_channel=Brackeys ^^ inspired

    //public List<Transform> targets;
    public Vector2 offset;
    public float smoothTime;
    private Vector2 velocity;
    public float minZoom = 50f;
    public float maxZoom = 20f;
    public float zoomLimiter = 30f;
    public float detectionRadius = 60f;
    public Collider2D player;
    public float CameraSpeed = 500f;


    public List<Collider2D> targets = new List<Collider2D>();
    ContactFilter2D filter = new ContactFilter2D();

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    
    private void Update()
    {
        if (player != null) 
        {
            targets.Add(player);
            Physics2D.OverlapCircle(player.transform.position, detectionRadius, filter, targets);
        } else 
        {
            if (GameObject.FindWithTag("Player") != null)
            {
                player = GameObject.FindWithTag("Player").GetComponentInChildren<Collider2D>();
                Debug.Log("Player reference: " + player);
            }
        }
        
    }
    
    


    void FixedUpdate() 
    {
        if (targets.Count == 0 || player == null)
        {
            return;
        }
        Move();
        Zoom();
    }


    void Move() 
    {
        Vector2 centerPoint = GetCenterPoint();
        //Vector2 newPosition = centerPoint + offset;
        /*
        Vector2 VectorToTargetPos = (Vector2) transform.position - centerPoint;
        float distToTargetPos = VectorToTargetPos.magnitude;
        Debug.Log(distToTargetPos);
        if (distToTargetPos > 10 || distToTargetPos < -10){
            transform.position = Vector2.SmoothDamp(transform.position, centerPoint, ref velocity, smoothTime);
        }
        */

        transform.position = Vector2.SmoothDamp(transform.position, centerPoint, ref velocity, smoothTime);
        //transform.position = Vector2.Lerp(transform.position, centerPoint, 0.6f*Time.deltaTime);
        //transform.position = Vector2.MoveTowards(transform.position, centerPoint, CameraSpeed*Time.deltaTime);

        
        //transform.position = centerPoint;
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDist()/zoomLimiter);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
        //cam.orthographicSize = 30;
    }

    float GetGreatestDist()
    {
        var bounds = new Bounds(player.transform.position, Vector2.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i].tag == "Landscape") {
                bounds.Encapsulate(targets[i].ClosestPoint(player.transform.position));
            } else {
                bounds.Encapsulate(targets[i].transform.position);
            }
        }
        bounds.Encapsulate(transform.position);
        return bounds.size.magnitude;
    }
    Vector2 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].transform.position;
        }

        var bounds = new Bounds(player.transform.position, Vector2.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null){
            }else if (targets[i].tag == "Landscape") {
                bounds.Encapsulate(targets[i].ClosestPoint(player.transform.position));
            }else if (targets[i].tag == "Bullet") {

            } else {
                bounds.Encapsulate(targets[i].transform.position);
            }
        }
        return bounds.center;
    }
}
