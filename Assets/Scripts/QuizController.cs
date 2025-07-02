using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    public Text questionText;
    public List<Button> answerButtons;

    public QuizQuestionSet questionSet; // Initialize questions

    private int currentQuestionIndex;
    private List<Question> questions;

    private bool quizCompleted;
    private NPCQuiz npcQuiz; // Reference to the associated NPCQuiz

    private string selectedLanguage;

    private int correctAnswers;
    private int wrongAnswers;

    [System.Serializable]

    public class Question
    {
        public string questionText;
        public List<string> answerOptions;
        public int correctAnswerIndex;
    }

    private void Start()
    {
        // Initialize with default language
        // InitializeQuestions();
    }

    public void SetLanguage(string language)
    {
        selectedLanguage = language;
    }

    public void StartQuiz(NPCQuiz quiz, QuizQuestionSet newQuestionSet)
    {
        correctAnswers = 0;
        wrongAnswers = 0;

        npcQuiz = quiz; // Store the correct NPCQuiz instance
        questionSet = newQuestionSet; // Assign the correct question set
        currentQuestionIndex = 0;
        quizCompleted = false;

        InitializeQuestions(); // Reload questions based on the new set

        QuizStatsService.Instance.StartQuizTimer();

        ShowQuestion(currentQuestionIndex);
    }


    private void InitializeQuestions()
    {
        if (questionSet != null)
        {
            Debug.Log($"QuizQuestionSet assigned: {questionSet.name}");
            questions = ConvertToQuizControllerQuestions(questionSet.questions);

            if (questions.Count == 0)
            {
                Debug.LogWarning($"No questions found for language: {selectedLanguage}");
            }
            else
            {
                Debug.Log($"Loaded {questions.Count} questions for language: {selectedLanguage}");
            }
        }
        else
        {
            Debug.LogWarning("QuizQuestionSet is not assigned.");
        }
    }

    private List<Question> ConvertToQuizControllerQuestions(List<QuizQuestion> quizQuestions)
    {
        List<Question> convertedQuestions = new List<Question>();
        Debug.Log($"Selected Language: {selectedLanguage}");
        foreach (var quizQuestion in quizQuestions)
        {

            if (quizQuestion.questionLang == selectedLanguage)
            {
                Debug.Log($"Checking question: {quizQuestion.questionText} (Language: {quizQuestion.questionLang})");
                Question convertedQuestion = new Question
                {
                    questionText = quizQuestion.questionText,
                    answerOptions = quizQuestion.answerOptions,
                    correctAnswerIndex = quizQuestion.correctAnswerIndex
                };

                convertedQuestions.Add(convertedQuestion);
            }
        }

        return convertedQuestions;
    }

    private void ShowQuestion(int questionIndex)
    {
        if (questionIndex >= 0 && questionIndex < questions.Count)
        {
            Question currentQuestion = questions[questionIndex];

            // Display the question text
            questionText.text = currentQuestion.questionText;

            // Display the answer options on buttons
            for (int i = 0; i < answerButtons.Count; i++)
            {
                if (i < currentQuestion.answerOptions.Count)
                {
                    answerButtons[i].gameObject.SetActive(true);
                    answerButtons[i].GetComponentInChildren<Text>().text = currentQuestion.answerOptions[i];

                    int answerIndex = i; // Capture the current value of i
                    answerButtons[i].onClick.RemoveAllListeners();
                    answerButtons[i].onClick.AddListener(() => OnAnswerSelected(answerIndex, currentQuestion.correctAnswerIndex));
                }
                else
                {
                    answerButtons[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogWarning("Question index out of range.");
        }
    }

    public bool IsQuizAnsweredCorrectly()
    {
        return quizCompleted;
    }

    private void OnAnswerSelected(int selectedAnswerIndex, int correctAnswerIndex)
    {
        if (quizCompleted)
        {
            Debug.Log("Quiz already completed.");
            return;
        }

        if (selectedAnswerIndex == correctAnswerIndex)
        {
            Debug.Log("Correct Answer!");
            currentQuestionIndex++;
            correctAnswers++;

            if (currentQuestionIndex < questions.Count)
            {
                // Display the next question
                ShowQuestion(currentQuestionIndex);
            }
            else
            {
                Debug.Log("Quiz completed!");
                quizCompleted = true;

                // Notify the NPCQuiz that the quiz is complete and correct
                NotifyNPCQuiz(true);
                QuizStatsService.Instance.CompleteQuiz(
                    questionSet.name,
                    selectedLanguage,
                    questions.Count,
                    correctAnswers,
                    wrongAnswers
                );

            }
        }
        else
        {
            Debug.Log("Wrong Answer. Try again.");
            wrongAnswers++;
            Debug.Log($"Wrong Answers so far: {wrongAnswers}");

            NotifyNPCQuiz(false); // Notify that the quiz was answered incorrectly
        }
    }

    public void CancelQuiz()
    {
        Debug.Log("Quiz was cancelled by the player.");

        // Just disable quiz mode, don't send a "wrong answer" back
        quizCompleted = false;

        if (npcQuiz != null)
        {
            npcQuiz.CancelQuiz(); // Call the method you created in NPCQuiz
        }
    }


    private void NotifyNPCQuiz(bool answeredCorrectly)
    {
        if (npcQuiz != null)
        {
            npcQuiz.OnQuizComplete(answeredCorrectly);
        }
        else
        {
            Debug.LogWarning("No NPCQuiz script found in the scene.");
        }
    }
}