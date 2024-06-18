using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Shcreepzy
{
    public class StageSelectManager : MonoBehaviour
    {
        [SerializeField] private Button quizButton;
        [SerializeField] private string quizScene;
        [SerializeField] private Button gameButton;
        [SerializeField] private string gameScene;
        [SerializeField] private Button backButton;
        [SerializeField] private string backScene;

        private void OnEnable()
        {
            quizButton.onClick.AddListener(OnQuizButtonClicked);
            gameButton.onClick.AddListener(OnGameButtonClicked);
            backButton.onClick.AddListener(OnBackButtonClicked);
        }

        private void OnDisable()
        {
            quizButton.onClick.RemoveListener(OnQuizButtonClicked);
            gameButton.onClick.RemoveListener(OnGameButtonClicked);
            backButton.onClick.RemoveListener(OnBackButtonClicked);
        }

        private void Start()
        {
#if UNITY_EDITOR
            if (GameDataManager.INSTANCE.IsDebugMode()) return;
#endif
            if (!GameDataManager.INSTANCE.data.finishedQuiz) gameButton.interactable = false;
        }

        private void OnQuizButtonClicked()
        {
            SceneManager.LoadScene(quizScene, LoadSceneMode.Single);
        }

        private void OnGameButtonClicked()
        {
            SceneManager.LoadScene(gameScene, LoadSceneMode.Single);
        }

        private void OnBackButtonClicked()
        {
            SceneManager.LoadScene(backScene, LoadSceneMode.Single);
        }
    }
}
