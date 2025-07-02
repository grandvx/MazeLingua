using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameTrigger : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            Debug.Log("Player entered the end zone. Ending game...");
            EndGame();
        }
    }

    private void EndGame()
    {
        // Option 1: Quit application (build only)
        //Application.Quit();

        // Option 2: Load an end scene (if you have one)
        // SceneManager.LoadScene("GameOverScene");
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndStatsScene");
        //         // For testing inside Unity Editor only
        // #if UNITY_EDITOR
        //         UnityEditor.EditorApplication.isPlaying = false;
        // #endif
    }
}
