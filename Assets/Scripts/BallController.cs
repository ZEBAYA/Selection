using UnityEngine;
using System.Collections.Generic;

public class BallController : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public float jumpForce = 8f; // Jump force
    public List<string> collectedColors = new List<string>(); // Tracks collected colors

    private Rigidbody2D rb;
    private bool isGrounded = true; // Check if the ball is grounded
    private int currentColorIndex = -1; // Tracks the current color in rewind
    private float moveDirection = 0; // Horizontal movement direction

    private ColorSource nearbyColorSource; // Tracks the color source the player is near
    [Header("Audio Settings")]
    public AudioSource collectionSound; // Sound effect for color collection

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Apply horizontal movement
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        // Collect color when 'G' is pressed
        if (Input.GetKeyDown(KeyCode.G))
        {
            CollectColor();
        }

        // Rewind color when 'H' is pressed
        if (Input.GetKeyDown(KeyCode.H))
        {
            RewindColor();
        }
    }

    public void MoveForward() { moveDirection = 1; }
    public void MoveBackward() { moveDirection = -1; }
    public void StopMoving() { moveDirection = 0; }
    public void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public void CollectColor()
    {
        if (nearbyColorSource != null)
        {
            string color = nearbyColorSource.colorName;
            if (!collectedColors.Contains(color))
            {
                collectedColors.Add(color);
                currentColorIndex = collectedColors.Count - 1; // Update to latest color
                ChangeColor(color);
                if (collectionSound != null)
                {
                    collectionSound.Play();
                }

                Destroy(nearbyColorSource.gameObject); // Optional: Remove color source

                // Increment score
                GameManager.Instance.UpdateScore(5);
            }
        }
    }

    public void RewindColor()
    {
        if (collectedColors.Count > 0)
        {
            currentColorIndex = (currentColorIndex - 1 + collectedColors.Count) % collectedColors.Count;
            ChangeColor(collectedColors[currentColorIndex]);
        }
        else
        {
            ResetToDefaultColor(); // Revert to white if no colors left
        }
    }

    public void RemoveColor(string color)
    {
        if (collectedColors.Contains(color))
        {
            collectedColors.Remove(color);

            if (collectedColors.Count > 0)
            {
                currentColorIndex = Mathf.Clamp(currentColorIndex, 0, collectedColors.Count - 1);
                ChangeColor(collectedColors[currentColorIndex]);
            }
            else
            {
                ResetToDefaultColor();
            }
        }
    }

    public void ChangeColor(string color)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && ColorUtility.TryParseHtmlString(color, out Color newColor))
        {
            spriteRenderer.color = newColor;
        }
        else
        {
            Debug.LogError("Failed to change color: " + color);
        }
    }
    public string GetCurrentColor()
    {
        if (collectedColors.Count > 0 && currentColorIndex >= 0 && currentColorIndex < collectedColors.Count)
        {
            return collectedColors[currentColorIndex];
        }
        return null; // Default to no color if no colors are collected
    }

    private void ResetToDefaultColor()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white; // Default color
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ColorSource"))
        {
            nearbyColorSource = collision.GetComponent<ColorSource>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ColorSource"))
        {
            if (collision.GetComponent<ColorSource>() == nearbyColorSource)
            {
                nearbyColorSource = null;
            }
        }
    }
}
