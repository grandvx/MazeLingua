using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LanguageSelectionMenu : MonoBehaviour
{
    public Canvas languageSelectionCanvas;
    public Button englishButton;
    public Button spanishButton;
    public Button frenchButton;
    public Button greekButton;

    private string selectedLanguage;

    void Start()
    {
        // Ensure the canvas is active at the start
        languageSelectionCanvas.gameObject.SetActive(true);

        // Unlock and show the cursor for UI interaction
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Set up button listeners
        englishButton.onClick.AddListener(() => OnLanguageSelected("English"));
        spanishButton.onClick.AddListener(() => OnLanguageSelected("Spanish"));
        frenchButton.onClick.AddListener(() => OnLanguageSelected("French"));
        greekButton.onClick.AddListener(() => OnLanguageSelected("Greek"));
    }

    void OnLanguageSelected(string language)
    {
        selectedLanguage = language;
        Debug.Log("Selected Language: " + selectedLanguage);

        // Deactivate the language selection canvas
        languageSelectionCanvas.gameObject.SetActive(false);

        // Lock and hide the cursor again
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Find and update all QuizController instances
        QuizController[] quizControllers = FindObjectsOfType<QuizController>();
        foreach (QuizController quizController in quizControllers)
        {
            quizController.SetLanguage(selectedLanguage);
        }
    }
}