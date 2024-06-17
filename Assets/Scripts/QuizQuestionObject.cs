using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shcreepzy
{
    public class QuizQuestionObject : MonoBehaviour
    {
        [Header("Question")]
        [SerializeField] private TextMeshProUGUI questionTextBox;
        [SerializeField] private Image questionImage;

        private QuizOptionType optionATtype;
        [Header("Option A")]
        [SerializeField] private TextMeshProUGUI optionATextBox;
        [SerializeField] private Image optionAImage;
        [SerializeField] private Button optionAButton;

        private QuizOptionType optionBTtype;
        [Header("Option B")]
        [SerializeField] private TextMeshProUGUI optionBTextBox;
        [SerializeField] private Image optionBImage;
        [SerializeField] private Button optionBButton;

        private QuizOptionType optionCTtype;
        [Header("Option C")]
        [SerializeField] private TextMeshProUGUI optionCTextBox;
        [SerializeField] private Image optionCImage;
        [SerializeField] private Button optionCButton;

        private QuizOptionType optionDTtype;
        [Header("Option D")]
        [SerializeField] private TextMeshProUGUI optionDTextBox;
        [SerializeField] private Image optionDImage;
        [SerializeField] private Button optionDButton;

        [HideInInspector] public QuizManager quizManager;

        private void OnEnable()
        {
            optionAButton.onClick.AddListener(OnOptionAClicked);
            optionBButton.onClick.AddListener(OnOptionBClicked);
            optionCButton.onClick.AddListener(OnOptionCClicked);
            optionDButton.onClick.AddListener(OnOptionDClicked);
        }

        private void OnDisable()
        {
            optionAButton.onClick.RemoveListener(OnOptionAClicked);
            optionBButton.onClick.RemoveListener(OnOptionBClicked);
            optionCButton.onClick.RemoveListener(OnOptionCClicked);
            optionDButton.onClick.RemoveListener(OnOptionDClicked);
        }

        private void OnOptionAClicked()
        {
            quizManager.OnOptionButtonClicked('A');
        }

        private void OnOptionBClicked()
        {
            quizManager.OnOptionButtonClicked('B');
        }

        private void OnOptionCClicked()
        {
            quizManager.OnOptionButtonClicked('C');
        }

        private void OnOptionDClicked()
        {
            quizManager.OnOptionButtonClicked('D');
        }

        public void SetQuestion(QuizQuestion question)
        {
            questionTextBox.text = question.QuestionText;
            if (question.QuestionHasImage)
            {
                questionImage.sprite = question.QuestionImage;
            }
            else
            {
                questionImage.gameObject.SetActive(false);
            }

            optionATtype = question.OptionAType;
            switch (question.OptionAType)
            {
                case QuizOptionType.TEXT:
                    {
                        optionATextBox.text = question.OptionAText;
                        optionAImage.gameObject.SetActive(false);
                    }
                    break;
                case QuizOptionType.IMAGE:
                    {
                        optionATextBox.gameObject.SetActive(false);
                        optionAImage.sprite = question.OptionAImage;
                    }
                    break;
            }

            optionBTtype = question.OptionBType;
            switch (question.OptionBType)
            {
                case QuizOptionType.TEXT:
                    {
                        optionBTextBox.text = question.OptionBText;
                        optionBImage.gameObject.SetActive(false);
                    }
                    break;
                case QuizOptionType.IMAGE:
                    {
                        optionBTextBox.gameObject.SetActive(false);
                        optionBImage.sprite = question.OptionBImage;
                    }
                    break;
            }

            optionCTtype = question.OptionCType;
            switch (question.OptionCType)
            {
                case QuizOptionType.TEXT:
                    {
                        optionCTextBox.text = question.OptionCText;
                        optionCImage.gameObject.SetActive(false);
                    }
                    break;
                case QuizOptionType.IMAGE:
                    {
                        optionCTextBox.gameObject.SetActive(false);
                        optionCImage.sprite = question.OptionCImage;
                    }
                    break;
            }

            optionDTtype = question.OptionDType;
            switch (question.OptionDType)
            {
                case QuizOptionType.TEXT:
                    {
                        optionDTextBox.text = question.OptionDText;
                        optionDImage.gameObject.SetActive(false);
                    }
                    break;
                case QuizOptionType.IMAGE:
                    {
                        optionDTextBox.gameObject.SetActive(false);
                        optionDImage.sprite = question.OptionDImage;
                    }
                    break;
            }
        }
    }
}
