using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shcreepzy
{
    public class BootManager : MonoBehaviour
    {
        [SerializeField] private string nextScene;

        private void Start()
        {
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        }
    }
}
