using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Shcreepzy
{
    public class StageSelectManager : MonoBehaviour
    {
        [SerializeField] private Button quizButton;
        [SerializeField] private SceneAsset quizScene;
        [SerializeField] private Button gameButton;
        [SerializeField] private SceneAsset gameScene;

        private void OnEnable()
        {
            quizButton.onClick.AddListener(OnQuizButtonClicked);
            gameButton.onClick.AddListener(OnGameButtonClicked);
        }

        private void OnDisable()
        {
            quizButton.onClick.RemoveListener(OnQuizButtonClicked);
            gameButton.onClick.RemoveListener(OnGameButtonClicked);
        }

        private void OnQuizButtonClicked()
        {
            SceneManager.LoadScene(quizScene.name);
        }

        private void OnGameButtonClicked()
        {
            SceneManager.LoadScene(gameScene.name);
        }
    }
}
