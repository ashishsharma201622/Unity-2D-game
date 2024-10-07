using System.Collections;
using UnityEngine;

public class SkeletonBehavior : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of enemy movement
    public float patrolDistance = 6f; // Distance to patrol from the starting position
    public LayerMask groundLayer; // Layer to check for ground

    private Animator animator; // Reference to Animator component
    private Vector3 startPosition; // Store the starting position
    private bool movingRight = true; // Control the direction of movement

    // Reference to respawn point
    public Transform respawnPoint;

    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component
        startPosition = transform.position; // Save the starting position
    }

    void Update()
    {
        Patrol(); // Call the patrol function
    }

    void Patrol()
    {
        // Move the skeleton right or left
        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime); // Move right
            if (transform.position.x >= startPosition.x + patrolDistance) // If it moves past the patrol distance
            {
                Flip(); // Flip to move left
            }
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime); // Move left
            if (transform.position.x <= startPosition.x - patrolDistance) // If it moves past the patrol distance
            {
                Flip(); // Flip to move right
            }
        }
    }

    void Flip()
    {
        movingRight = !movingRight; // Change direction
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Flip the skeleton on the X-axis
        transform.localScale = scale;
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
                Debug.Log("Player landed on the skeleton!"); // Debug log

                // Deactivate the eagle GameObject
                gameObject.SetActive(false);
            }
            else
            {


            Debug.Log("Skeleton collided with the player!"); // Debug log

            // Deactivate the player GameObject
            collision.gameObject.SetActive(false); // Deactivate the player GameObject

            // Start coroutine to respawn the player
            StartCoroutine(RespawnPlayer(collision.gameObject));
		}

            // Make the skeleton immovable by freezing its position
            Rigidbody2D skeletonRigidbody = GetComponent<Rigidbody2D>();
		
            if (skeletonRigidbody != null)
            {
                skeletonRigidbody.constraints = RigidbodyConstraints2D.FreezeAll; // Freeze all movements
            }
        }
    }

    private IEnumerator RespawnPlayer(GameObject player)
    {
        yield return new WaitForSeconds(3f); // Wait for 3 seconds before respawning

        // Move the player to the respawn point
        player.transform.position = respawnPoint.position;
        player.SetActive(true); // Reactivate the player GameObject
        Debug.Log("Player respawned at the respawn point."); // Log respawn event
    }
}

