using System.Collections.Generic;
using UnityEngine;

namespace Shcreepzy
{
    public class QuizManager : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private QuizQuestionObject quizQuestionPrefab;
        private QuizQuestionObject currentQuizQuestionObject;

        [SerializeField] private List<QuizQuestion> quizQuestions;
        private int currentQuestionIndex;

        private void Start()
        {
            currentQuestionIndex = 0;

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

            if (optionIsCorrect)
            {
                Debug.Log("Correct");
                Object.Destroy(currentQuizQuestionObject.gameObject);
                currentQuestionIndex++;
                if (currentQuestionIndex >= quizQuestions.Count) { Debug.Log("No more questions"); return; }
                SpawnNextQuestion();
            }
            else
            {
                Debug.Log("Wrong, try again");
            }
        }

        // 1. quiz manager spawns ques prefab
        // 2. give the new ques object a reference to the quiz manager
        // 3. tell the new ques object to tell the quiz manager if they get clicked
    }
}
