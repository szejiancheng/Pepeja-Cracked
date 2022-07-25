using UnityEngine;

public class CameraColliderResize : MonoBehaviour
{
    public Camera Cam;
    public BoxCollider2D Box;
    public float offset;

    void Update()
    {
        if (!Cam.orthographic)
        {
            Debug.LogError("Camera must be Orthographic.");
            return;
        }

        var aspect = (float)Screen.width / Screen.height;
        var orthoSize = Cam.orthographicSize;

        var width = 2.0f * orthoSize * aspect - offset;
        var height = 2.0f * Cam.orthographicSize - offset;

        Box.size = new Vector2(width, height);
    }
}