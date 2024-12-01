using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void Restart()
    {
        // Get the name of the current active scene
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Main Menu")
        {
            Debug.LogWarning("Cannot restart the Main Menu!");
            return; // Prevent restarting the Main Menu
        }

        Debug.Log($"Restarting scene: {currentScene}");
        SceneManager.LoadScene(currentScene); // Reload the current scene
    }
}
