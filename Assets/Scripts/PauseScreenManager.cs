using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Shcreepzy
{
    public class PauseScreenManager : MonoBehaviour
    {
        public static PauseScreenManager INSTANCE { get; private set; }

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
            if (INSTANCE != null)
            {
                Debug.LogError("Attempting to instantiate multiple copies of singleton PauseScreenManager, self-destructing");
                Object.Destroy(this.gameObject);
            }
            else
            {
                INSTANCE = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        // TODO: There's probably a better way than this
        public void Die()
        {
            INSTANCE = null;
            Object.Destroy(this.gameObject);
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
            Die();
            SceneManager.LoadScene(menuScene);
        }
    }
}
