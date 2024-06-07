using System.Collections.Generic;
using UnityEngine;

namespace Shcreepzy
{
    public class QuizManager : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private QuizQuestionObject quizQuestionPrefab;
        private QuizQuestionObject currentQuizQuestionObject;

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

            if (currentQuizQuestionObject != null)
            {
                Debug.LogWarning("currentQuestion is not null for some reason, destroying object");
                Object.Destroy(currentQuizQuestionObject);
            }

            SpawnNextQuestion();
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
