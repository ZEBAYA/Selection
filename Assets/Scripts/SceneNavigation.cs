using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    // Function to load the Main Menu scene
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
