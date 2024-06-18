using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Shcreepzy
{
    public class PauseScreenManager : MonoBehaviour
    {
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private string menuScene;

        [SerializeField] private Button pauseButton;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button menuButton;

        private void OnEnable()
        {
            pauseButton.onClick.AddListener(OnPauseButtonClicked);
            resumeButton.onClick.AddListener(OnResumeButtonClicked);
            menuButton.onClick.AddListener(OnMenuButtonClicked);
        }

        private void OnDisable()
        {
            pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
            resumeButton.onClick.RemoveListener(OnResumeButtonClicked);
            menuButton.onClick.RemoveListener(OnMenuButtonClicked);
        }

        private void Start()
        {
            // Crossing the DontDestroyOnLoad border results in broken references
            // TODO: find a better way to do this
            pauseScreen = GameObject.Find("PauseScreen");
            pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
            resumeButton = GameObject.Find("ResumtButton").GetComponent<Button>();
            menuButton = GameObject.Find("MenuButton").GetComponent<Button>();
        }

        private void OnPauseButtonClicked()
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }

        private void OnResumeButtonClicked()
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }

        private void OnMenuButtonClicked()
        {
            LevelObjectiveManager.INSTANCE.Die();
            PersistentCanvas.INSTANCE.Die();
            SceneManager.LoadScene(menuScene);
        }
    }
}
