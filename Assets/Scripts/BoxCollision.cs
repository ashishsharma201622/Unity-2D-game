using UnityEngine;
using System.Collections;

public class BoxCollision : MonoBehaviour
{
    private Vector3 originalPosition; // Store the original position of the box
    void Start()
    {
        // Store the initial position of the box
        originalPosition = transform.position;
    }

    // This method will be called when the player collides with the box
    public void StartFalling()
    {
        Debug.Log("Start falling called");
        StartCoroutine(FallAfterDelay(1f)); // Start the coroutine with a 1-second delay
    }

    private IEnumerator FallAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay

        // Apply gravity to make the box fall
        Rigidbody2D rb = GetComponent<Rigidbody2D>(); // Use Rigidbody2D
        if (rb != null)
        {
            rb.isKinematic = false; // Disable kinematic to allow falling
            // Note: No need to set useGravity, Rigidbody2D uses gravity scale
        }
    }

    // Method to reset the box's position
    public void ResetBoxPosition()
    {
        transform.position = originalPosition; // Reset the position to the original position
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        Debug.Log("Resetting box position");

        if (rb != null)
        {
            rb.isKinematic = true; // Reset kinematic to true to stop it from falling again
            rb.velocity = Vector2.zero; // Optional: Reset any velocity to ensure it doesn't move unexpectedly

        }
    }
}
