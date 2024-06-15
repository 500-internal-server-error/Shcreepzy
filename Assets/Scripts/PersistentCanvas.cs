using UnityEngine;

namespace Shcreepzy
{
    public class PersistentCanvas : MonoBehaviour
    {
        public static PersistentCanvas INSTANCE { get; private set; }

        private void Start()
        {
            if (INSTANCE != null)
            {
                Debug.LogError("Attempting to initialize multiple copies of singleton PersistentCanvas, self-destructing");
                Object.Destroy(this.gameObject);
                return;
            }
            else
            {
                INSTANCE = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }
}
