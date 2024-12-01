using UnityEngine;

public class ColorSource : MonoBehaviour
{
    public string colorName;         // The color associated with this source
    private bool playerNearby = false; // Tracks if the player is near
    private BallController ballController;

    private void Start()
    {
        ballController = FindObjectOfType<BallController>();
        if (ballController == null)
        {
            Debug.LogError("No BallController found in the scene!");
        }
    }

    private void Update()
    {
        // Collect color with key press
        if (playerNearby && Input.GetKeyDown(KeyCode.G))
        {
            CollectColor();
        }
    }

    private void CollectColor()
    {
        if (playerNearby && ballController != null)
        {
            Debug.Log($"Player collected color: {colorName}");

            // Add the color to the ball's collected colors
            if (!ballController.collectedColors.Contains(colorName))
            {
                ballController.collectedColors.Add(colorName);
                Debug.Log($"Color {colorName} added to ball's collection.");
            }
            else
            {
                Debug.LogWarning($"Color {colorName} is already collected.");
            }

            // Change the ball's appearance to match the collected color
            ballController.ChangeColor(colorName);

            // Optional: Destroy the color source after collection
           // Destroy(gameObject, 0.5f); // Destroy with a slight delay for smooth transitions
        }
        else
        {
            Debug.LogWarning("Player is not near the color source or ball controller is missing.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Player is near the color source: " + colorName);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearby = false;
            Debug.Log("Player left the color source: " + colorName);
        }
    }
}
