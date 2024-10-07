using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollision : MonoBehaviour
{
    public GameObject RoadToHome; // Assign this in the Inspector

    private void Start()
    {
        // Set finish1 to inactive initially
        RoadToHome.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collision with Key detected!");
            gameObject.SetActive(false); // Deactivate the Key object
 	    RoadToHome.SetActive(true);
        }
    }
}
