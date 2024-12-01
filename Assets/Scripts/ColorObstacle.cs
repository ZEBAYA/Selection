using UnityEngine;
using System.Collections;

public class ColorObstacle : MonoBehaviour
{
    public string requiredColor;         // The color required to pass the obstacle
    public Animator animator;            // Animator to control obstacle animations
    private Collider2D obstacleCollider; // Collider to block the player
    public float horizontalDelay = 2f;   // Delay for horizontal obstacles
    public float verticalDelay = 0f;     // Delay for vertical obstacles
    public bool isHorizontal = true;     // Set in inspector to define orientation

    private bool isProcessing = false;   // Prevent multiple triggers during delay
    [Header("Audio Settings")]
    public AudioSource obstaclePassSound; // Sound effect for passing obstacle

    private void Start()
    {
        obstacleCollider = GetComponent<Collider2D>();

        if (obstacleCollider == null)
        {
            Debug.LogError("Obstacle Collider2D is missing!");
        }

        if (animator == null)
        {
            Debug.LogWarning("Animator is not assigned. Obstacle will not animate.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !isProcessing)
        {
            BallController ballController = collision.collider.GetComponent<BallController>();

            if (ballController != null)
            {
                // Check if the player is actively displaying the required color
                if (ballController.GetCurrentColor() == requiredColor)
                {
                    float delay = isHorizontal ? horizontalDelay : verticalDelay; // Choose delay based on orientation
                    StartCoroutine(HandleInteractionWithDelay(ballController, delay));
                }
                // Play sound effect
                if (obstaclePassSound != null)
                {
                    obstaclePassSound.Play();
                }
                else
                {
                    Debug.LogWarning($"The player does not have the required color ({requiredColor}) to unlock this obstacle!");
                    GameManager.Instance.UpdateScore(-3); // Decrement score
                }
            }
        }
    }

    private IEnumerator HandleInteractionWithDelay(BallController ballController, float delay)
    {
        isProcessing = true; // Prevent re-triggering during delay
        yield return new WaitForSeconds(delay); // Wait for the delay

        // Check if the player still has the required color
        if (ballController.collectedColors.Contains(requiredColor))
        {
            Debug.Log("Obstacle unlocked with color: " + requiredColor);

            PlayOpenAnimation();
            ballController.RemoveColor(requiredColor); // Remove the used color
        }
        else
        {
            Debug.LogWarning($"You need the {requiredColor} color to pass!");
        }

        isProcessing = false; // Allow interaction again
    }

    private void PlayOpenAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Open"); // Trigger the "Open" animation
        }

        // Disable the collider to allow the player to pass
        if (obstacleCollider != null)
        {
            obstacleCollider.enabled = false;
        }

        // Optional: Close the obstacle after a delay
        Invoke(nameof(CloseObstacle), 2f); // Change delay if needed
    }

    private void CloseObstacle()
    {
        if (animator != null)
        {
            animator.SetTrigger("Close"); // Trigger the "Close" animation
        }

        // Re-enable the collider to block the player again
        if (obstacleCollider != null)
        {
            obstacleCollider.enabled = true;
        }
    }
}
