using System.Collections.Generic;
using UnityEngine;

using SystemRandom = System.Random;

namespace Shcreepzy
{
    public class GameDataManager : MonoBehaviour
    {
        public struct GameData
        {
            public SystemRandom rng;
            public bool finishedQuiz;
        }

        public static GameDataManager INSTANCE { get; private set; }

        public GameData data;

        private void Start()
        {
            if (INSTANCE != null)
            {
                Debug.LogError("Attempting to instantiate multiple copies of singleton GameDataManager, self-destructing");
                Object.Destroy(this.gameObject);
            }
            else
            {
                INSTANCE = this;
                DontDestroyOnLoad(this.gameObject);
            }

            data.rng = new SystemRandom();
            data.finishedQuiz = false;
        }
    }
}
