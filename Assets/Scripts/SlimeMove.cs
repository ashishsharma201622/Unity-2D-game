using System.Collections;
using UnityEngine;

public class SlimeMove : MonoBehaviour
{
    public float moveSpeed = 0.06f; // Speed of enemy movement
    public float patrolDistance = 6f; // Distance to patrol from the starting position
    public LayerMask groundLayer; // Layer to check for ground

    private Animator animator; // Reference to Animator component
    private Vector3 startPosition; // Store the starting position

    // Reference to respawn point
    public Transform respawnPoint;

    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component
        startPosition = transform.position; // Save the starting position
    }

    void Update()
    {
        // Check for ground
        bool isGrounded = Physics2D.OverlapCircle(transform.position, 0.1f, groundLayer);


    }

    // Handle collision events
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Slime collided with the player!"); // Debug log

            // Deactivate the player GameObject
            collision.gameObject.SetActive(false); // Deactivate the player GameObject

            // Start coroutine to respawn the player
            StartCoroutine(RespawnPlayer(collision.gameObject));

	   // Make the bear immovable by freezing its position
           Rigidbody2D bearRigidbody = GetComponent<Rigidbody2D>();
           if (bearRigidbody != null)
     	   {
            bearRigidbody.constraints = RigidbodyConstraints2D.FreezeAll; // Freeze all movements
           }

        }
    }

    private IEnumerator RespawnPlayer(GameObject player)
    {
        yield return new WaitForSeconds(3f); // Wait for 3 seconds before respawning (adjust as needed)

        // Move the player to the respawn point
        player.transform.position = respawnPoint.position; 
        player.SetActive(true); // Reactivate the player GameObject
        Debug.Log("Player respawned at the respawn point."); // Log respawn event
    }
}
