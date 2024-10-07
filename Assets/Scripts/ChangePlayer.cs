using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayer : MonoBehaviour
{
    // The amount of force to apply for the jump (adjust this based on your game)
    public float jumpForce = 3000f; // This should be enough to simulate a 100 ft jump based on Unity's scale

    // This method is called when a collision occurs
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object colliding is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            MakePlayerJump(collision.gameObject);
        }
    }

    // Method to make the player jump
    private void MakePlayerJump(GameObject player)
    {
        // Get the Rigidbody2D component from the player
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        // If the player has a Rigidbody2D component, apply an upward force
        if (rb != null)
        {
            // Zero out any existing Y velocity before applying the jump force
            rb.velocity = new Vector2(rb.velocity.x, 0);

            // Apply upward force for the jump
            rb.AddForce(new Vector2(0, jumpForce));
        }
    }
}
