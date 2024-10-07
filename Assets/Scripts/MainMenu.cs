using UnityEngine;
using UnityEngine.SceneManagement; // This lets you switch scenes

public class MainMenu : MonoBehaviour
{
    // This function will be called when the Play button is pressed
    public void PlayGame()
    {
        // Load the next scene (Make sure your game scene is added in Build Settings)
        SceneManager.LoadScene("GameScene"); // "GameScene" should be the name of your game scene
    }

    // Call this for quitting the game (useful for Exit button)
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit(); // This will close the application
    }
}
