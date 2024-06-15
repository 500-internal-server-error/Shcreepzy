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
        [SerializeField] private List<LevelObjective> levelObjectives;
        [SerializeField] private ObjectiveListUI objectiveListUI;
        private int currentObjectiveIndex;
        private int? lastObjectiveIndex;
        private float timeSinceLastObjective;

        private void OnValidate()
        {
            foreach (LevelObjective objective in levelObjectives)
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
            timeSinceLastObjective = 0;
        }

        private void Update()
        {
            timeSinceLastObjective += Time.deltaTime;
        }

        public void OnObstacleEnter(Transform hitObstacle)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void OnLevelObjectiveEnter(LevelObjective hitLevelObjective)
        {
            if (hitLevelObjective.transform.position == levelObjectives[currentObjectiveIndex].transform.position)
            {
                if (timeSinceLastObjective - hitLevelObjective.GetMinimumTimeSinceLastObjective() < 0)
                {
                    Debug.Log("objective hit but is too fast");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    return;
                }

                if (hitLevelObjective.ShouldPopObjective()) objectiveListUI.PopObjective();
                Debug.Log("objective hit, moving to next objective");
                lastObjectiveIndex = currentObjectiveIndex;
                currentObjectiveIndex++;
                if (currentObjectiveIndex >= levelObjectives.Count)
                {
                    Debug.Log("no more objectives");
                    return;
                }
                timeSinceLastObjective = 0;
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
