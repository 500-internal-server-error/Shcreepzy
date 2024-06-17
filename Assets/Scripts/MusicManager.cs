using UnityEngine;

namespace Shcreepzy
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager INSTANCE { get; private set; }

        private void Start()
        {
            if (INSTANCE != null)
            {
                Debug.LogError("Attempting to initialize multiple copies of singleton MusicManager, self-destructing");
                Object.Destroy(this.gameObject);
            }
            else
            {
                INSTANCE = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }
}
