using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Shcreepzy
{
    public class ThanksManager : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private SceneAsset scene;

        private void OnEnable()
        {
            button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            SceneManager.LoadScene(scene.name);
        }
    }
}
