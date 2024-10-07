using UnityEngine;
using System.Collections;

public class WoodCollision : MonoBehaviour
{
    private Vector3 originalPosition; // Store the original position of the wood object
    private bool isMoving = true; // Flag to control movement

    void Start()
    {
        // Store the initial position of the wood
        originalPosition = transform.position;

        // Start the movement coroutine immediately
        StartCoroutine(MoveUpAndDown());
    }

    // Coroutine to move the wood up and down
    private IEnumerator MoveUpAndDown()
    {
        // Randomize the movement parameters
        float moveSpeed = Random.Range(0.5f, 6f); // Random speed between 0.5 and 2 units per second
        float moveDistance = Random.Range(5f, 10f); // Random distance between 5 and 10 units
        float waitTime = Random.Range(1f, 6f); // Random wait time between 1 and 3 seconds

        Vector3 targetPosition = originalPosition + new Vector3(0, moveDistance, 0); // Calculate the target position

        // Move up and down indefinitely
        while (isMoving)
        {
            // Move upwards
            while (transform.position.y < targetPosition.y && isMoving)
            {
                transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0); // Move up
                yield return null; // Wait for the next frame
            }

            // Wait for a random time before moving down
            yield return new WaitForSeconds(waitTime);

            // Move down
            while (transform.position.y > originalPosition.y && isMoving)
            {
                transform.position -= new Vector3(0, moveSpeed * Time.deltaTime, 0); // Move down
                yield return null; // Wait for the next frame
            }

            // Wait for a random time before moving up again
            yield return new WaitForSeconds(waitTime);
        }
    }

    // Method to stop the movement if needed
    public void StopMoving()
    {
        isMoving = false; // Set flag to stop movement
    }

    // Optional: Reset the wood's position if needed
    public void ResetWoodPosition()
    {
        transform.position = originalPosition; // Reset the position to the original position
    }
}
