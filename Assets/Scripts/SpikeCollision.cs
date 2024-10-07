using UnityEngine;
using System.Collections;


public class SpikeCollision : MonoBehaviour
{

    // Reference to respawn point
    public Transform respawnPoint;




    // Handle collision events
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Spike collided with the player!"); // Debug log

            // Deactivate the player GameObject
            collision.gameObject.SetActive(false); // Deactivate the player GameObject

            // Start coroutine to respawn the player
            StartCoroutine(RespawnPlayer(collision.gameObject));

        }
    }

    private IEnumerator RespawnPlayer(GameObject player)
    {
        yield return new WaitForSeconds(3f); // Wait for 3 seconds before respawning (adjust as needed)

        // Move the player to the respawn point
        player.transform.position = new Vector3(respawnPoint.position.x, respawnPoint.position.y, respawnPoint.position.z);
        player.SetActive(true); // Reactivate the player GameObject
        Debug.Log("Player respawned at the respawn point."); // Log respawn event
    }
}
