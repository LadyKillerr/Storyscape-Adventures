using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [Header("Config Settings")]
    [SerializeField] QuestionSO questionScript;
    [SerializeField] int currentIndex = 0;

    // question Text
    [SerializeField] TextMeshProUGUI questionText;

    // questions arrays
    [SerializeField] QuestionSO[] questions;

    [Header("Answer Buttons")]
    [SerializeField] TextMeshProUGUI[] answers;
    [SerializeField] Color defaultAnswerColor;
    [SerializeField] Color correctAnswerColor;
    int correctAnswerIndex;
    

    void Awake()
    {
        questionText.text = questions[currentIndex].GetQuestion();

        for (int i = 0; i < answers.Length; i++)
        {
            answers[i].text = questions[currentIndex].GetAnswer(i); 
        }
    }

    public void OnAnswerSelected(int index)
    {
        if (index == questionScript.GetCorrectAnswerIndex())
        {

        }
    }

    void LoadNextQuestion()
    {
        currentIndex++;
    }

    void CheckCorrectAnswersIndex()
    {
        //switch (questionScript.GetCorr)

    }
    
}
