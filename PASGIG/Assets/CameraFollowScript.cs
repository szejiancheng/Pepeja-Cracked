using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Transform target;
    public Vector2 offset;
    [Range(1, 10)]
    public float smoothFactor;

    private void FixedUpdate() {
        Follow();
    }

    void Follow()
    {
        Vector2 targetPos = target.position + (Vector3)offset;
        Vector2 smoothPos = Vector2.Lerp(transform.position, targetPos, smoothFactor*Time.fixedDeltaTime);
        transform.position = targetPos;
    }
}
