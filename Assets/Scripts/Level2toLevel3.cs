using UnityEngine;

using System.Collections;


public class Level2toLevel3 : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed at which the object moves upward
    private bool isMoving = false; // Flag to control upward movement

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("vhayooo");
            isMoving = true; // Start moving upwards
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Stop moving when the player exits the collision
        if (collision.gameObject.CompareTag("Player"))
        {
            isMoving = false; // Stop moving upwards
        }
    }

    private void Update()
    {
        // Move upwards if the flag is set
        if (isMoving)
        {
            transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0); // Move up
        }
    }
}
