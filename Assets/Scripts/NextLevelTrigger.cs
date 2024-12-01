using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    public string mainMenuSceneName = "Main Menu"; // Name of the main menu scene

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Ensure only the player can trigger
        {
            Debug.Log("Player reached the end of the level!");
            LoadNextLevelOrMainMenu();
        }
    }

    private void LoadNextLevelOrMainMenu()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Check if there is a next scene in the build settings
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            // Load the next level
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            // If this is the last level, transition to the main menu
            Debug.Log("All levels completed. Returning to main menu.");
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }
}
