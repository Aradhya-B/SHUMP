using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks whether a GameObject is on screen and can force it to stay on the screen.
/// Note that this only works for an orthographic Main Camera at [0, 0, 0].
/// </summary>
public class BoundsCheck : MonoBehaviour
{
    // Set in Inspector
    public float radius = 1f;
    public bool keepOnScreen = false;

    // Set Dynamically
    public bool isOnScreen = true;
    public float camWidth;
    public float camHeight;

    [HideInInspector]
    public bool offRight, offLeft, offUp, offDown;

    void Awake()
    {
        // Get the distance from the origin of the world to the top or bottom edge of the screen
        camHeight = Camera.main.orthographicSize;
        // Multiply the height by the screens aspect ratio to get the width
        camWidth = camHeight * Camera.main.aspect;
    }

    // LateUpdate() is called  every frame after Update() has been called for every other GameObject
    // Using LateUpdate() here avoids creating a race condition between the Update() function in the Hero script
    void LateUpdate()
    {
        Vector3 pos = transform.position;
        isOnScreen = true;
        offRight = offLeft = offDown = offUp = false;
        
        // Right side
        if (pos.x > camWidth - radius)
        {
            pos.x = camWidth - radius;
            offRight = true;
        }

        // Left side
        if (pos.x < -camWidth + radius)
        {
            pos.x = -camWidth + radius;
            offLeft = true;
        }

        // Top edge
        if (pos.y > camHeight - radius)
        {
            pos.y = camHeight - radius;
            offUp = true;
        }

        // Bottom Edge
        if (pos.y < -camHeight + radius)
        {
            pos.y = -camHeight + radius;
            offDown = true;
        }

        isOnScreen = !(offRight || offLeft || offUp || offDown);

        if (keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            isOnScreen = true;
            offRight = offLeft = offDown = offUp = false;
        }
    }

    // Draw the bounds in the Scene pane using OnDrawGizmos()
    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(camWidth * 2, camHeight * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }
}
