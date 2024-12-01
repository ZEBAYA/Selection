using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    public string winSceneName = "Win"; // Name of the win scene

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Ensure only the player can trigger
        {
            Debug.Log("Player reached the end of the level!");
            LoadNextLevelOrWinScene();
        }
    }

    private void LoadNextLevelOrWinScene()
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
            // If this is the last level, transition to the win scene
            Debug.Log("All levels completed. Loading Win Scene.");
            SceneManager.LoadScene(winSceneName);
        }
    }
}
