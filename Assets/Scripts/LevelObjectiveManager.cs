using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Shcreepzy
{
    public class LevelObjectiveManager : MonoBehaviour
    {
        public static LevelObjectiveManager INSTANCE { get; private set; }

        [SerializeField] private LayerMask levelObjectiveLayer;
        [SerializeField] private List<Collider> levelObjectives;
        private int currentObjectiveIndex;
        private int? lastObjectiveIndex;

        private void OnValidate()
        {
            foreach (Collider objective in levelObjectives)
            {
                if (1 << objective.gameObject.layer != levelObjectiveLayer)
                {
                    Debug.LogWarning($"LevelObjective object {objective.gameObject.name} is on layer {1 << objective.gameObject.layer} instead of layer {levelObjectiveLayer.value}!");
                }
            }
        }

        private void Start()
        {
            if (INSTANCE != null)
            {
                Debug.LogError("Attempting to instantiate multiple copies of singleton LevelObjectiveManager, self-destructing");
                Object.Destroy(this.gameObject);
                return;
            }
            else
            {
                INSTANCE = this;
                DontDestroyOnLoad(this.gameObject);
            }

            currentObjectiveIndex = 0;
            lastObjectiveIndex = null;
        }

        public void OnObstacleEnter(Transform hitObstacle)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void OnLevelObjectiveEnter(Transform hitLevelObjective)
        {
            if (hitLevelObjective.position == levelObjectives[currentObjectiveIndex].transform.position)
            {
                Debug.Log("objective hit, moving to next objective");
                lastObjectiveIndex = currentObjectiveIndex;
                currentObjectiveIndex++;
                if (currentObjectiveIndex >= levelObjectives.Count) Debug.Log("no more objectives");
            }
            else
            {
                Debug.Log("objective hit but is not current");
            }
        }

        public Transform GetSpawnLocation()
        {
            if (lastObjectiveIndex == null) return null;
            // Once again not smart enough to tell its definitely not null at this point
            return levelObjectives[lastObjectiveIndex ?? 0].transform;
        }
    }
}
