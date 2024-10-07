using UnityEngine;

public class PowerOff : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Debug message to confirm collision
            Debug.Log("2D collision with PowerOff detected!");

            // Get the PlayerMovement component from the player
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            
             // Start the speed boost OFF coroutine
            StartCoroutine(playerMovement.SpeedBoostOff());

            // Destroy the PowerOff after the collision
            Destroy(gameObject);
        }
    }
}
