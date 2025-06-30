using UnityEngine;
using TMPro; // If using TextMeshPro

public class GameStatsDisplay : MonoBehaviour
{
    public TextMeshProUGUI totalQuestionsText;
    public TextMeshProUGUI correctAnswersText;
    public TextMeshProUGUI wrongAnswersText;
    public TextMeshProUGUI timeTakenText;

    void Start()
    {
        totalQuestionsText.text = $"Total Questions: {StatsManager.Instance.totalQuestions}";
        correctAnswersText.text = $"Correct Answers: {StatsManager.Instance.correctAnswers}";
        wrongAnswersText.text = $"Wrong Answers: {StatsManager.Instance.wrongAnswers}";
        timeTakenText.text = $"Time Taken: {StatsManager.Instance.GetFormattedTime()}";
    }
}
