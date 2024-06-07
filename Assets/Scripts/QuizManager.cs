using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Shcreepzy
{
    public class QuizManager : MonoBehaviour
    {
        [Header("References")]

        [SerializeField] private Canvas canvas;
        [SerializeField] private TextMeshProUGUI timer;
        [SerializeField] private QuizQuestionObject quizQuestionPrefab;
        private QuizQuestionObject currentQuizQuestionObject;

        [Header("Quiz Timer Settings")]

        [SerializeField, Min(0), Tooltip("Quiz time in seconds")] private int maxQuizTime;
        private int currentQuizTime;
        private float lastSecond;

        [Header("Quiz Question Settings")]

        [SerializeField, Min(0)] private int minimumCorrectAnswers;
        private int correctAnswers;

        [SerializeField] private List<QuizQuestion> quizQuestions;
        private int currentQuestionIndex;

        private void OnValidate()
        {
            minimumCorrectAnswers = Mathf.Clamp(minimumCorrectAnswers, 0, quizQuestions.Count);
        }

        private void Start()
        {
            currentQuestionIndex = 0;
            correctAnswers = 0;
            currentQuizTime = maxQuizTime;
            lastSecond = 0;

            if (currentQuizQuestionObject != null)
            {
                Debug.LogWarning("currentQuestion is not null for some reason, destroying object");
                Object.Destroy(currentQuizQuestionObject.gameObject);
            }

            SpawnNextQuestion();
        }

        private void Update()
        {
            lastSecond += Time.deltaTime;
            while (lastSecond >= 1)
            {
                lastSecond--;
                currentQuizTime--;
            }

            if (currentQuizTime <= 0)
            {
                currentQuizTime = 0;
                if (currentQuizQuestionObject != null)
                {
                    Debug.Log($"Out of time, {correctAnswers}/{quizQuestions.Count}");
                    Object.Destroy(currentQuizQuestionObject.gameObject);
                    currentQuizQuestionObject = null;
                }
            }

            int minutes = currentQuizTime / 60;
            int seconds = currentQuizTime % 60;
            timer.text = $"{(minutes < 10 ? $"0{minutes}" : minutes)}:{(seconds < 10 ? $"0{seconds}" : seconds)}";
        }

        private void SpawnNextQuestion()
        {
            currentQuizQuestionObject = Object.Instantiate(quizQuestionPrefab, canvas.transform);
            currentQuizQuestionObject.quizManager = this;

            currentQuizQuestionObject.SetQuestion(quizQuestions[currentQuestionIndex]);
        }

        public void OnOptionButtonClicked(char option)
        {
            bool optionIsCorrect = option switch
            {
                'A' => quizQuestions[currentQuestionIndex].OptionAIsCorrect,
                'B' => quizQuestions[currentQuestionIndex].OptionBIsCorrect,
                'C' => quizQuestions[currentQuestionIndex].OptionCIsCorrect,
                'D' => quizQuestions[currentQuestionIndex].OptionDIsCorrect,
                _ => false
            };

            if (optionIsCorrect) correctAnswers++;

            Object.Destroy(currentQuizQuestionObject.gameObject);
            currentQuestionIndex++;
            if (currentQuestionIndex >= quizQuestions.Count) { Debug.Log($"{correctAnswers}/{quizQuestions.Count}"); return; }
            SpawnNextQuestion();
        }
    }
}
