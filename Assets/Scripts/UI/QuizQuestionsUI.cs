using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizQuestionsUI : MonoBehaviour
{
    [SerializeField] GameObject quizSection;
    QuestionManager questionManager;
    TextMeshProUGUI questionNumberText;

    int totalQuestions;
    int currentQuestion;

    void Awake()
    {
        // get ra question Manager của quizSection
        questionManager = quizSection.GetComponent<QuestionManager>();

        // Get ra UI questionNumber hiện tại
        questionNumberText = GetComponent<TextMeshProUGUI>();

        // Get ra tổng số câu hỏi trong mảng
        totalQuestions = questionManager.GetTotalQuestions();
    }

    void Update()
    {
        ShowQuizProgress();
    }

    void ShowQuizProgress()
    {
        currentQuestion = questionManager.GetCurrentIndex() + 1;


        questionNumberText.text = "Question " + currentQuestion.ToString("00") + " / " + totalQuestions.ToString("00");
    }
}
