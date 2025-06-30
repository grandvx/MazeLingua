using UnityEngine;

public class StatsManager : MonoBehaviour
{
    // Singleton pattern
    public static StatsManager Instance;

    // Quiz stats
    public string quizName;
    public string language;
    public int totalQuestions;
    public int correctAnswers;
    public int wrongAnswers;

    // Time tracking
    private float startTime;
    private float endTime;
    public float timeTaken;

    private void Awake()
    {
        // Ensure only one StatsManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Call this when starting a quiz
    public void StartTimer()
    {
        startTime = Time.time;
    }

    // Call this when quiz is completed
    public void EndTimer()
    {
        endTime = Time.time;
        timeTaken = endTime - startTime;
    }

    // Reset all stats before a new quiz starts
    public void ResetStats()
    {
        quizName = "";
        language = "";
        totalQuestions = 0;
        correctAnswers = 0;
        wrongAnswers = 0;
        timeTaken = 0f;
        startTime = 0f;
        endTime = 0f;
    }

    // Returns time in MM:SS format
    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(timeTaken / 60F);
        int seconds = Mathf.FloorToInt(timeTaken % 60F);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Optional: log stats to console or Unity Analytics
    public void LogStats()
    {
        Debug.Log($"Quiz Name: {quizName}");
        Debug.Log($"Language: {language}");
        Debug.Log($"Total Questions: {totalQuestions}");
        Debug.Log($"Correct Answers: {correctAnswers}");
        Debug.Log($"Wrong Answers: {wrongAnswers}");
        Debug.Log($"Time Taken: {GetFormattedTime()}");

        // TODO: Send to Unity Cloud Analytics if desired
    }
}
