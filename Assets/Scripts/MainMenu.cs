using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject sceneSelectionCanvas; // Assign in inspector

    public void PlayGame()
    {
        sceneSelectionCanvas.SetActive(true); // Show the canvas

    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    // These are called by the Easy/Medium/Hard buttons
    public void LoadEasyScene()
    {
        SceneManager.LoadScene(1); // Replace with your actual scene name
    }

    public void LoadMediumScene()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadHardScene()
    {
        SceneManager.LoadScene(3);
    }
}