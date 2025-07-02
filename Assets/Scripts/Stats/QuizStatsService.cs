using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Analytics;
using System.Text;

public class QuizStatsService : MonoBehaviour
{
    public static QuizStatsService Instance;

    public List<QuizStat> sessionStats = new List<QuizStat>();
    private bool sessionStarted = false;
    private float sessionStartTime;
    private float totalTimeSpent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            sessionStartTime = Time.time;
            totalTimeSpent = 0f;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private float currentQuizStartTime;

    public void StartQuizTimer()
    {
        currentQuizStartTime = Time.time;

        if (!sessionStarted)
        {
            sessionStartTime = Time.time;
            sessionStarted = true;
        }
    }

    public void CompleteQuiz(string quizName, string language, int totalQuestions, int correct, int wrong)
    {
        float timeTaken = Time.time - currentQuizStartTime;
        totalTimeSpent += timeTaken;

        QuizStat stat = new QuizStat
        {
            quizName = quizName,
            language = language,
            totalQuestions = totalQuestions,
            correctAnswers = correct,
            wrongAnswers = wrong,
            timeTaken = timeTaken,
            timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        sessionStats.Add(stat);

        LogToAnalytics(stat);
        SaveToCSV(stat);
    }

    private void LogToAnalytics(QuizStat stat)
    {
        Analytics.CustomEvent("quiz_completed", new Dictionary<string, object>
        {
            { "quizName", stat.quizName },
            { "language", stat.language },
            { "totalQuestions", stat.totalQuestions },
            { "correctAnswers", stat.correctAnswers },
            { "wrongAnswers", stat.wrongAnswers },
            { "timeTaken", stat.timeTaken }
        });
    }

    // ✅ Optionally keep this if CSV export is desired
    private void SaveToCSV(QuizStat stat)
    {
        string filePath = Path.Combine(Application.persistentDataPath, "QuizStats.csv");
        bool fileExists = File.Exists(filePath);

        StringBuilder sb = new StringBuilder();

        if (!fileExists)
        {
            sb.AppendLine("Timestamp,QuizName,Language,TotalQuestions,CorrectAnswers,WrongAnswers,TimeTakenSeconds");
        }

        sb.AppendLine($"{stat.timestamp},{stat.quizName},{stat.language},{stat.totalQuestions},{stat.correctAnswers},{stat.wrongAnswers},{stat.timeTaken:F2}");

        File.AppendAllText(filePath, sb.ToString());
    }

    // ✅ Accessors for total session stats
    public int GetSessionTotalQuestions()
    {
        int total = 0;
        foreach (var stat in sessionStats)
            total += stat.totalQuestions;
        return total;
    }

    public int GetSessionCorrectAnswers()
    {
        int total = 0;
        foreach (var stat in sessionStats)
            total += stat.correctAnswers;
        return total;
    }

    public int GetSessionWrongAnswers()
    {
        int total = 0;
        foreach (var stat in sessionStats)
            total += stat.wrongAnswers;
        return total;
    }

    public float GetSessionTotalTime()
    {
        return totalTimeSpent;
    }

    public void ResetSessionStats()
    {
        sessionStats.Clear();
        totalTimeSpent = 0f;
        sessionStartTime = Time.time;
    }
    public float GetTotalSessionTime()
    {
        if (!sessionStarted) return 0f;
        return Time.time - sessionStartTime;
    }

    public IReadOnlyList<QuizStat> GetSessionStats()
    {
        return sessionStats.AsReadOnly();
    }
}

public class QuizStat
{
    public string quizName;
    public string language;
    public int totalQuestions;
    public int correctAnswers;
    public int wrongAnswers;
    public float timeTaken;
    public string timestamp;
}
