using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuiz : MonoBehaviour
{
    public static NPCQuiz activeQuiz; // Static reference to the active quiz
    public QuizController quizController; // Reference to QuizController
    public DoorAOpen[] doors; // Array to store multiple doors
    public Canvas quizCanvas; // Reference to the Canvas for the quiz UI

    private bool quizActive = false; // Track whether the quiz is active
    private bool playerInRange = false; // Track if the player is near the NPC
    public QuizQuestionSet assignedQuizQuestionSet; // Assign this in Unity Inspector

    void Start()
    {
        // Ensure only this canvas is activated
        foreach (NPCQuiz npc in FindObjectsOfType<NPCQuiz>())
        {
            if (npc != this && npc.quizCanvas != null)
            {
                npc.quizCanvas.gameObject.SetActive(false);
            }
        }

    }

    void Update()
    {
        // Check if the player presses the interact key and is near the NPC
        if (playerInRange && Input.GetKeyDown(KeyCode.E)) // Example: "E" to interact
        {
            if (!quizActive)
            {
                StartQuiz();
            }
        }

        // Cancel quiz with Escape
        if (quizActive && Input.GetKeyDown(KeyCode.Escape))
        {
            CancelQuiz();
        }
    }

    void StartQuiz()
    {
        // If another quiz is active, prevent starting this one
        if (activeQuiz != null && activeQuiz != this)
        {
            Debug.Log("Another quiz is already active.");
            return;
        }

        activeQuiz = this; // Set this quiz as active
        quizActive = true;

        if (quizController != null && quizCanvas != null)
        {
            quizController.StartQuiz(this, assignedQuizQuestionSet); // Pass the current NPCQuiz instance
            quizCanvas.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void CancelQuiz()
    {
        quizActive = false;

        if (quizCanvas != null)
        {
            quizCanvas.gameObject.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (activeQuiz == this)
        {
            activeQuiz = null;
        }

        Debug.Log("Quiz cancelled. Returned to gameplay.");
    }


    public void OnQuizComplete(bool answeredCorrectly)
    {
        quizActive = false;

        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (answeredCorrectly)
        {
            Debug.Log("Quiz answered correctly! Opening the doors.");
            quizCanvas.gameObject.SetActive(false);

            foreach (DoorAOpen door in doors)
            {
                if (door != null)
                {
                    door.OpenDoor();
                }
            }
        }
        else
        {
            quizCanvas.gameObject.SetActive(false);
            Debug.Log("Quiz answered incorrectly. Try again.");
        }

        // Clear the active quiz
        if (activeQuiz == this)
        {
            activeQuiz = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure only the player triggers this
        {
            playerInRange = true;
            Debug.Log("Player in range of NPC. Press E to start the quiz.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure only the player triggers this
        {
            playerInRange = false;
            Debug.Log("Player left NPC range.");
        }
    }
}
