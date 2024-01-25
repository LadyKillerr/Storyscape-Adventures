using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    [Header("Questions Section")]
    [SerializeField] int currentIndex = 0;

    // question Text
    [SerializeField] TextMeshProUGUI questionText;

    // questions arrays
    [SerializeField] QuestionSO[] questions;

    [Header("Answers Section")]
    [SerializeField] GameObject[] answers;
    TextMeshProUGUI answersText;
    [SerializeField] Color wrongAnswerColor;
    [SerializeField] Color correctAnswerColor;

    [Header("Audio Section")]
    [SerializeField] AudioClip wrongAnswerAudio;
    [SerializeField][Range(0, 1)] float wrongAudio;

    [Header("Quiz Settings")]
    [SerializeField] bool isAnswered;
    [SerializeField] float delayTime = 2f;

    // Components that are hidden
    AudioSource quizSectionAudio;

    void Awake()
    {
        quizSectionAudio = GetComponent<AudioSource>();

        isAnswered = false;

        // Get ra câu hỏi theo index
        questionText.text = questions[currentIndex].GetQuestion();

        for (int i = 0; i < answers.Length; i++)
        {
            answersText = answers[i].GetComponentInChildren<TextMeshProUGUI>();

            answersText.text = questions[currentIndex].GetAnswer(i);
        }


        
    }

    public void OnAnswerSelected(int userAnswerIndex)
    {
        // nếu trả lời đúng
        if (userAnswerIndex == questions[currentIndex].GetCorrectAnswerIndex() && isAnswered == false)
        {
            // làm hiệu ứng tung hoa bằng particles

            // chạy âm thanh câu trả lời đúng 

            // khi câu trả lời đúng khoá chức năng nút luôn 
            isAnswered = true;

            // chuyển màu của nút bấm sang màu xanh
            answers[userAnswerIndex].GetComponent<Image>().color = correctAnswerColor;

        }
        else // nếu trả lời sai 
        {
            // tạm thời khoá nút lại khoảng chừng 2s
            isAnswered = true;
            Invoke("ResetIsAnswered", delayTime);

            // làm màn hình rung lắc


            // chạy hiệu ứng âm thanh trả lời sai

            

            // chuyển màu của nút bấm
            answers[userAnswerIndex].GetComponent<Image>().color = wrongAnswerColor;


        }
    }

    void LoadNextQuestion()
    {
        currentIndex++;
    }


    public int GetTotalQuestions()
    {
        return questions.Length;
    }

    public int GetCurrentIndex()
    {
        return currentIndex;
    }

    void ResetIsAnswered()
    {
        isAnswered = false;
    }
}
