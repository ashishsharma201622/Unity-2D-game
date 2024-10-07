using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public float jumpForce = 5f; // Force applied for jumping
    public Transform groundCheck; // Position to check if player is grounded
    public LayerMask groundLayer; // Layer to check for ground

    public GameObject[] respawnPoints; // Array to store respawn points
    private GameObject currentCheckpoint; // Track the current checkpoint

    private Animator animator; // Reference to Animator component
    private Rigidbody2D rb; // Reference to Rigidbody2D component
    private bool isGrounded; // Check if player is grounded
    private Vector3 originalScale; // Store the original scale of the player
    private float fallTime; // Timer for how long the player has been falling

    private static PlayerMovement instance;
    private bool isSpeedBoosted = false;



    void Start()
    {
        animator = GetComponent<Animator>(); // Access the Animator
        rb = GetComponent<Rigidbody2D>(); // Access the Rigidbody2D
        originalScale = transform.localScale; // Save the original scale

        // Find all respawn points tagged with "Respawn"
        respawnPoints = GameObject.FindGameObjectsWithTag("Respawn");

        // Initialize the first checkpoint as the current checkpoint
        currentCheckpoint = respawnPoints.Length > 0 ? respawnPoints[0] : null;
    }

    void Update()
    {
        HandleMovement();
        HandleJumping();
        HandleFalling();
        EnsureFacingDown();
    }

    private void HandleMovement()
    {
        // Get horizontal movement input
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Set isRunning true when moving, false when stopping
        bool isRunning = Mathf.Abs(moveHorizontal) > 0.1f;
        animator.SetBool("isRunning", isRunning);

        // Move the player using Rigidbody2D
        Vector2 movement = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);
        rb.velocity = movement;

        // Flip the sprite based on movement direction while keeping the original scale
        if (moveHorizontal < 0)
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z); // Flip left
        else if (moveHorizontal > 0)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z); // Flip right

        // Check if player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // Sync isGrounded with Animator
        animator.SetBool("isGrounded", isGrounded);
    }

    private void HandleJumping()
    {
        // Jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Apply jump force
            animator.SetTrigger("isJumping"); // Trigger jump animation
        }
    }

    private void HandleFalling()
    {
        // Handle falling animation
        if (!isGrounded && rb.velocity.y < 0)
        {
            animator.SetBool("isFalling", true); // Trigger falling animation
            fallTime += Time.deltaTime; // Increment fall timer
        }
        else
        {
            animator.SetBool("isFalling", false);
            fallTime = 0; // Reset fall timer when grounded
        }

        // Check if the player has fallen for 1.5 seconds
        if (fallTime >= 1.5f)
        {
            RespawnPlayer(); // Call respawn function
        }
    }

    private void EnsureFacingDown()
    {
        // Ensure player faces downwards when falling
        if (rb.velocity.y < -0.1f && !isGrounded)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0); // Reset rotation to face down
        }
    }

    public IEnumerator SpeedBoost()
    {
        if (isSpeedBoosted) yield break; // Exit if already boosted

        isSpeedBoosted = true; // Set the flag to true

        // Store the original speed
        float originalSpeed = moveSpeed;
        Debug.Log($"Original Speed: {originalSpeed}");

        // Increase the player's speed
        moveSpeed = 15f;
	jumpForce = 9f;
        Debug.Log($"Boosted Speed: {moveSpeed}");

        // Wait for 10 seconds
        yield return new WaitForSeconds(5);

        // Reset the player's speed back to original
        moveSpeed = originalSpeed; // Reset to original speed
        Debug.Log($"Reset Speed: {moveSpeed}");

        isSpeedBoosted = false; // Reset the flag
    }

    public IEnumerator SpeedBoostOff()
    {
        Debug.Log("Speed boost off called");

        isSpeedBoosted = false; // Set the flag to false to indicate boost is off

        // Reset the player's speed back to the original speed
        moveSpeed = 5f; // You might want to reference 'originalSpeed' if that variable is being used elsewhere

        Debug.Log($"Speed Reset to: {moveSpeed}");

        // Optionally, yield return null to wait for the next frame
        yield return null; // This allows the coroutine to pause execution until the next frame
    }

    // Handle collision events
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object the player collided with is tagged "Respawn"
        if (collision.gameObject.CompareTag("Respawn"))
        {
            Debug.Log("Player hit a respawn point!");
            RespawnPlayer();
        }

        // Check if the object the player collided with is tagged "FloatingBox"
        if (collision.gameObject.CompareTag("FloatingBox"))
        {
            Debug.Log("Player hit a FloatingBox");
            BoxCollision boxCollision = collision.gameObject.GetComponent<BoxCollision>();
            if (boxCollision != null)
            {
                boxCollision.StartFalling(); // Trigger the falling behavior
            }
        }
    }

    private void RespawnPlayer()
    {
        // Find the nearest checkpoint
        if (respawnPoints.Length > 0)
        {
            Transform nearestCheckpoint = respawnPoints[0].transform; // Assume the first checkpoint is the nearest initially
            float nearestDistance = Vector3.Distance(transform.position, nearestCheckpoint.position);

            for (int i = 1; i < respawnPoints.Length; i++)
            {
                float distance = Vector3.Distance(transform.position, respawnPoints[i].transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestCheckpoint = respawnPoints[i].transform; // Update the nearest checkpoint
                }
            }

            // Move the player to the nearest checkpoint
            transform.position = nearestCheckpoint.position;
            Debug.Log("Player respawned at: " + nearestCheckpoint.position);

            // Reactivate all boxes
            GameObject[] boxes = GameObject.FindGameObjectsWithTag("FloatingBox"); // Assuming boxes have the tag "FloatingBox"
            foreach (GameObject box in boxes)
            {
                Debug.Log("Resetting box: " + box.name); // Debug log for resetting boxes
                BoxCollision boxCollision = box.GetComponent<BoxCollision>();
                if (boxCollision != null)
                {
                    boxCollision.ResetBoxPosition(); // Reactivate the box
                }
            }
        }
    }
}
