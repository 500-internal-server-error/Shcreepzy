using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shcreepzy
{
    public class QuizQuestionObject : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI questionTextBox;
        [SerializeField] private TextMeshProUGUI optionATextBox;
        [SerializeField] private TextMeshProUGUI optionBTextBox;
        [SerializeField] private TextMeshProUGUI optionCTextBox;
        [SerializeField] private TextMeshProUGUI optionDTextBox;
        [SerializeField] private Button optionAButton;
        [SerializeField] private Button optionBButton;
        [SerializeField] private Button optionCButton;
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
            questionTextBox.text = question.Question;
            optionATextBox.text = question.OptionA;
            optionBTextBox.text = question.OptionB;
            optionCTextBox.text = question.OptionC;
            optionDTextBox.text = question.OptionD;
        }
    }
}
