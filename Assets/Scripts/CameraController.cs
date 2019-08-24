using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float dragSpeed = 1f;
    public float scrollSpeed = 3f;
    public float minSize = 5f;
    public float maxSize = 10f;
    public float boundingRadius = 20f;
    private Vector3 dragOrigin;

    void Update()
    {
        if (Input.GetMouseButtonDown(2)) 
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        float scroll = -Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            float size = Camera.main.orthographicSize;
            size += scroll * scrollSpeed;
            size = Mathf.Clamp(size, minSize, maxSize);
            Camera.main.orthographicSize = size;
        }

        if (!Input.GetMouseButton(2)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);
        Vector3 newPos = transform.position + move;

        if (newPos.x > boundingRadius)
            move.x = 0.05f;
        else if (newPos.x < -boundingRadius)
            move.x = -0.05f;
        
        if (newPos.y > boundingRadius)
            move.y = 0.05f;
        else if (newPos.y < -boundingRadius)
            move.y = -0.05f;
        
        transform.Translate(-move, Space.World);

    }
}
