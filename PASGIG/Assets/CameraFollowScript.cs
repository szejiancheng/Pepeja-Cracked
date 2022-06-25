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


    public List<Collider2D> targets = new List<Collider2D>();
    ContactFilter2D filter = new ContactFilter2D();

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        targets.Add(player);
    }

    
    private void Update()
    {
        Physics2D.OverlapCircle(player.transform.position, detectionRadius, filter, targets);
        //foreach (Collider2D collider in targets) {
        //    Debug.Log(collider);
        //}
        targets.Add(player);
    }
    
    


    void LateUpdate() 
    {
        if (targets.Count == 0)
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
        transform.position = Vector2.MoveTowards(transform.position, centerPoint, smoothTime);
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
            bounds.Encapsulate(targets[i].transform.position);
        }
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
            bounds.Encapsulate(targets[i].transform.position);
        }
        return bounds.center;
    }
}
