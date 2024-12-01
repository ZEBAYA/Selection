using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels. Game completed!");
        }
    }

    public void EndGame()
    {
        Debug.Log("Game Over");
        // Implement game-over logic here
    }
    public AudioSource backgroundTone;

    private void Start()
    {
        if (backgroundTone != null)
        {
            backgroundTone.loop = true;
            backgroundTone.Play();
        }
        else
        {
            Debug.LogError("Background tone is not assigned!");
        }
    }

}
