using UnityEngine;
using TMPro;

public class GameStatsDisplay : MonoBehaviour
{
    public TextMeshProUGUI totalQuestionsText;
    public TextMeshProUGUI correctAnswersText;
    public TextMeshProUGUI wrongAnswersText;
    public TextMeshProUGUI timeTakenText;

    void Start()
    {
        if (QuizStatsService.Instance.sessionStats.Count == 0)
        {
            totalQuestionsText.text = "Total Questions: 0";
            correctAnswersText.text = "Correct Answers: 0";
            wrongAnswersText.text = "Wrong Answers: 0";
            timeTakenText.text = "Time Taken: 00:00";
            return;
        }

        var stats = QuizStatsService.Instance;

        int totalQuestions = stats.GetSessionTotalQuestions();
        int totalCorrect = stats.GetSessionCorrectAnswers();
        int totalWrong = stats.GetSessionWrongAnswers();
        float totalSessionTime = stats.GetTotalSessionTime();

        totalQuestionsText.text = $"Total Questions: {totalQuestions}";
        correctAnswersText.text = $"Correct Answers: {totalCorrect}";
        wrongAnswersText.text = $"Wrong Answers: {totalWrong}";
        timeTakenText.text = $"Total Session Time: {FormatTime(totalSessionTime)}";
    }


    string FormatTime(float seconds)
    {
        int min = Mathf.FloorToInt(seconds / 60f);
        int sec = Mathf.FloorToInt(seconds % 60f);
        return $"{min:00}:{sec:00}";
    }
}
