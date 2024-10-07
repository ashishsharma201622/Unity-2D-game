using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleAttack : MonoBehaviour
{
    public float patrolHeight = 2f; // Height the eagle will patrol up and down
    public float patrolSpeed = 0.9f;  // Speed of the up-down movement
    private Vector3 startPosition;  // Store the starting position

    // Reference to respawn point (you can set this in the inspector)
    public Transform respawnPoint;

    void Start()
    {
        // Save the starting position of the eagle
        startPosition = transform.position;
    }

    void Update()
    {
        // Move the eagle up and down continuously in a sinusoidal pattern
        float newY = Mathf.Sin(Time.time * patrolSpeed) * patrolHeight;
        transform.position = new Vector3(startPosition.x, startPosition.y + newY, transform.position.z);
    }

    // Handle collision events
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the contact point of the collision
            ContactPoint2D contact = collision.GetContact(0);

            // Check if the player is above the eagle when the collision happens
            if (contact.point.y > transform.position.y)
            {
                Debug.Log("Player landed on the eagle!"); // Debug log

                // Deactivate the eagle GameObject
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Eagle collided with the player from the side!"); // Debug log

                // Deactivate the player GameObject
                collision.gameObject.SetActive(false); // Deactivate the player GameObject

                // Start coroutine to respawn the player
                StartCoroutine(RespawnPlayer(collision.gameObject));
            }
        }
    }

    // Coroutine to respawn the player after a delay
    private IEnumerator RespawnPlayer(GameObject player)
    {
        yield return new WaitForSeconds(3f); // Wait for 3 seconds before respawning

        // Move the player to the respawn point
        if (respawnPoint != null)
        {
            player.transform.position = respawnPoint.position; 
        }

        // Reactivate the player GameObject
        player.SetActive(true); 
        Debug.Log("Player respawned at the respawn point."); // Log respawn event
    }
}
