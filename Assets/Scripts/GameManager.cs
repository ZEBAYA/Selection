using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton instance
    public TextMeshProUGUI scoreText;       // Reference to the score UI text
    private int score = 0;        // Current score
    public AudioSource backgroundTone;

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene load events
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from scene load events
    }

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

        // Try to find the ScoreText in the current scene
        AssignScoreText();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reassign ScoreText whenever a new scene is loaded
        AssignScoreText();
    }

    private void AssignScoreText()
    {
        if (scoreText == null)
        {
            scoreText = GameObject.Find("scoreText")?.GetComponent<TextMeshProUGUI>();

            if (scoreText == null)
            {
                Debug.LogError("Score Text is not assigned in the GameManager or could not be found in the scene!");
                return;
            }

            // Update the score display with the current score
            UpdateScoreDisplay();
        }
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        UpdateScoreDisplay();
    }

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

   

    public void Quit()
    {
        // Quit the application
        Application.Quit();
        Debug.Log("Quit");

        // For testing in the Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }


}
