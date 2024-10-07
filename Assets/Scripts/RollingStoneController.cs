using UnityEngine;
using System.Collections;

public class RollingStoneController : MonoBehaviour
{
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private bool isRolling = false; // Check if the RollingStone is rolling

    private void Start()
    {
        // Get the Rigidbody2D component from the current GameObject (RollingStone)
        rb = GetComponent<Rigidbody2D>();

        // Check if the Rigidbody2D is found
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component missing on RollingStone GameObject.");
            this.enabled = false; // Disable the script if Rigidbody2D is missing
            return;
        }

        rb.isKinematic = true; // Make the RollingStone initially not affected by physics
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isRolling = true;  // Start rolling the stone
            rb.isKinematic = false; // Allow physics to affect the RollingStone
            Debug.Log("RollingStone started rolling."); // Log to confirm rolling starts
            
            // Optionally uncheck (reset) isRolling after a delay or based on certain conditions
            // For example, to stop rolling after 3 seconds
            StartCoroutine(StopRollingAfterDelay(3f));
        }

        if (collision.gameObject.CompareTag("Enemies"))
        {
	    collision.gameObject.SetActive(false); // Deactivate the player GameObject

        }
    }

    // Coroutine to stop rolling after a specified delay
    private IEnumerator StopRollingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified time
        isRolling = false; // Reset the rolling state
        Debug.Log("RollingStone stopped rolling."); // Log to confirm stopping
    }

    private void FixedUpdate()
    {
        // If the RollingStone should roll, apply a force and torque to it
        if (isRolling)
        {
            // Apply force to roll the stone
            rb.AddForce(Vector2.right * 10f); // Adjust the force value as needed
            // Apply torque to simulate rolling
            rb.AddTorque(-5f); // Adjust the torque value for a natural roll effect
        }
    }
}
