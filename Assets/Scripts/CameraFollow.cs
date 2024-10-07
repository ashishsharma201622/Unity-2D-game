
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float smoothSpeed = 0.125f; // Smoothing speed
    public Vector3 offset; // Offset position for the camera

    void LateUpdate()
    {
        // Check if the player reference still exists
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset; // Calculate the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Smooth movement
            transform.position = smoothedPosition; // Update camera position
        }
    }
}
