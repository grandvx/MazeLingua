using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quiz Question Set", menuName = "Quiz Question Set")]
public class QuizQuestionSet : ScriptableObject
{
    // List of quiz questions
    public List<QuizQuestion> questions = new List<QuizQuestion>();
}

[System.Serializable]
public class QuizQuestion
{
    // Question text
    public string questionText;

    // Question language
    public string questionLang;

    // List of answer options
    public List<string> answerOptions;

    // Index of the correct answer (0-based)
    public int correctAnswerIndex;
}
