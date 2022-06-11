using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    //TODO: Make dynamic camera resize to include objects of interest
    //TODO: optimize

    //https://www.youtube.com/watch?v=aLpixrPvlB8&ab_channel=Brackeys ^^ inspired

    public List<Transform> targets;
    public Vector2 offset;
    public float smoothTime = 0.5f;
    private Vector2 velocity;
    public float minZoom = 50f;
    public float maxZoom = 20f;
    public float zoomLimiter = 30f;


    //TODO: make use of Physics.Overlap to create a dynamic array to render
    Collider[] PointsOfInterest;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }
    
    /*
    private void Update()
    {
        Physics.OverlapCircleNonAlloc(targets[0].position, 50f, PointsOfInterest);
        for(int i = 0; i < PointsOfInterest.Length; i++) {
            Debug.Log(PointsOfInterest[i]);
        }
    }
    */

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
        Vector2 newPosition = centerPoint + offset;
        //transform.position = Vector2.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
        transform.position = newPosition;
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDist()/zoomLimiter);
        cam.orthographicSize = newZoom;
    }

    float GetGreatestDist()
    {
        var bounds = new Bounds(targets[0].position, Vector2.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.x;
    }
    Vector2 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector2.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }
}
