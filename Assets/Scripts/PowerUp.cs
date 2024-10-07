using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Debug message to confirm collision
            Debug.Log("2D collision with PowerUp detected!");

            // Get the PlayerMovement component from the player
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            
            if (playerMovement != null)
            {
                // Start the speed boost coroutine
                StartCoroutine(playerMovement.SpeedBoost());
            }

            // Destroy the PowerUp after the collision
            Destroy(gameObject);
        }
    }
}
