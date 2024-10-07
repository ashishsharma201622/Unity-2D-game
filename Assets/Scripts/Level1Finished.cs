using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Import the SceneManager

public class Level1Finished : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision with Home detected!");
            
            // Deactivate the Player object
            collision.gameObject.SetActive(false); 
            
            // Load the next scene after a short delay
            StartCoroutine(LoadNextScene(collision.gameObject));
        }
    }

    // Coroutine to load the next scene
    IEnumerator LoadNextScene(GameObject player)
    {
        yield return new WaitForSeconds(1f); // Optional: small delay before loading the scene

        // Load the next scene
        SceneManager.LoadScene("gamescene1");

        // Reposition the player in the new scene
        player.SetActive(true); // Reactivate the player in the new scene
        player.transform.position = new Vector3(0, 0, 0); // Set the player's position to (0,0,0)
    }
}
