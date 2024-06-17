using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Shcreepzy
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private SceneAsset playScene;
        [SerializeField] private Button materialButton;
        [SerializeField] private SceneAsset materialScene;
        [SerializeField] private Button exitButton;

        private void OnEnable()
        {
            playButton.onClick.AddListener(OnPlayButtonClicked);
            materialButton.onClick.AddListener(OnMaterialButtonClicked);
            exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        private void OnDisable()
        {
            playButton.onClick.RemoveListener(OnPlayButtonClicked);
            materialButton.onClick.RemoveListener(OnMaterialButtonClicked);
            exitButton.onClick.RemoveListener(OnExitButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            SceneManager.LoadScene(playScene.name, LoadSceneMode.Single);
        }

        private void OnMaterialButtonClicked()
        {
            SceneManager.LoadScene(materialScene.name, LoadSceneMode.Single);
        }

        private void OnExitButtonClicked()
        {
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
            Application.Quit();
#else
            // Quit is bad for mobile, we probably should just not have this button at all on any platform
            // https://docs.unity3d.com/2023.2/Documentation/ScriptReference/Application.Quit.html
            Debug.Log("quit");
#endif
        }
    }
}
