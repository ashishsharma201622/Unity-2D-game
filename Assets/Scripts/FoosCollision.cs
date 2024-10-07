using UnityEngine;

public class FoodCollision : MonoBehaviour
{
    public GameObject[] foodItems; // Assign all food items in the Inspector

    private int foodCount; // To keep track of the number of active food items

    private void Start()
    {
        // Initialize the food count with the number of active food items
        foodCount = foodItems.Length;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collision with Food detected!");
            gameObject.SetActive(false); // Deactivate the food object
            foodCount--; // Decrease the food count
        }
    }
}
