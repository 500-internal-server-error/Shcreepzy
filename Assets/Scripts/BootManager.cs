using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shcreepzy
{
    public class BootManager : MonoBehaviour
    {
        [SerializeField] private SceneAsset nextScene;

        private void Start()
        {
            SceneManager.LoadScene(nextScene.name);
        }
    }
}
