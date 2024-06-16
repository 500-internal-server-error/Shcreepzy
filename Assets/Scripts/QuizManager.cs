using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Shcreepzy
{
    public class QuizManager : MonoBehaviour
    {
        [Header("References")]

        [SerializeField] private Canvas canvas;
        [SerializeField] private SceneAsset mainMenuScene;

        [Header("References - Main View")]

        [SerializeField] private RectTransform mainView;
        [SerializeField] private TextMeshProUGUI timer;
        [SerializeField] private QuizQuestionObject quizQuestionPrefab;
        private QuizQuestionObject currentQuizQuestionObject;

        [Header("References - Win View")]

        [SerializeField] private RectTransform winView;
        [SerializeField] private TextMeshProUGUI winScore;
        [SerializeField] private Button winButton;

        [Header("References - Lose View")]

        [SerializeField] private RectTransform loseView;
        [SerializeField] private TextMeshProUGUI loseScore;
        [SerializeField] private Button loseButton;

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

        private void OnEnable()
        {
            winButton.onClick.AddListener(OnWinButtonClicked);
            loseButton.onClick.AddListener(OnLoseButtonClicked);
        }

        private void OnDisable()
        {
            winButton.onClick.RemoveListener(OnWinButtonClicked);
            loseButton.onClick.RemoveListener(OnLoseButtonClicked);
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

            mainView.gameObject.SetActive(true);
            winView.gameObject.SetActive(false);
            loseView.gameObject.SetActive(false);

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
                    Object.Destroy(currentQuizQuestionObject.gameObject);
                    currentQuizQuestionObject = null;

                    mainView.gameObject.SetActive(false);
                    var (view, score) = correctAnswers >= minimumCorrectAnswers ? (winView, winScore) : (loseView, loseScore);
                    view.gameObject.SetActive(true);
                    score.text = $"{correctAnswers}/{quizQuestions.Count}";
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
            if (currentQuestionIndex >= quizQuestions.Count)
            {
                mainView.gameObject.SetActive(false);
                var (view, score) = correctAnswers >= minimumCorrectAnswers ? (winView, winScore) : (loseView, loseScore);
                view.gameObject.SetActive(true);
                score.text = $"{correctAnswers}/{quizQuestions.Count}";
                return;
            }
            SpawnNextQuestion();
        }

        public void OnWinButtonClicked()
        {
            GameDataManager.INSTANCE.data.finishedQuiz = true;
            SceneManager.LoadScene(mainMenuScene.name, LoadSceneMode.Single);
        }

        public void OnLoseButtonClicked()
        {
            GameDataManager.INSTANCE.data.finishedQuiz = false;
            SceneManager.LoadScene(mainMenuScene.name, LoadSceneMode.Single);
        }
    }
}
