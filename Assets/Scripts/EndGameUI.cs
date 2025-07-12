using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameUI : MonoBehaviour
{

    void Start()
    {
        // Unlock and show the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void GoToMainMenu()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Current Scene Index: " + currentSceneIndex);

        // Range: scene 1 to 3 → if at 3, go to Main Menu (scene 0)
        if (currentSceneIndex < 3)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0); // Go back to Main Menu
        }
    }
    public void QuitGame()
    {
        Debug.Log("Quit button pressed.");
        Application.Quit();

#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in the editor
#endif
    }
}
