using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Shcreepzy
{
    public class LevelObjectiveManager : MonoBehaviour
    {
        public static LevelObjectiveManager INSTANCE { get; private set; }

        [SerializeField] private LayerMask levelObjectiveLayer;
        [SerializeField] private List<Collider> levelObjectives;
        private int currentObjectiveIndex;

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
            }

            currentObjectiveIndex = 0;

            foreach (Collider objective in levelObjectives)
            {
                if (1 << objective.gameObject.layer != levelObjectiveLayer)
                {
                    Debug.LogWarning($"LevelObjective object {objective.gameObject.name} is on layer {objective.gameObject.layer} instead of layer {levelObjectiveLayer.value}!");
                }
            }
        }

        public void OnLevelObjectiveEnter(Transform hitLevelObjective)
        {
            if (hitLevelObjective.position == levelObjectives[currentObjectiveIndex].transform.position)
            {
                Debug.Log("objective hit, moving to next objective");
                currentObjectiveIndex++;
                if (currentObjectiveIndex >= levelObjectives.Count) Debug.Log("no more objectives");
            }
            else
            {
                Debug.Log("objective hit but is not current");
            }
        }
    }
}
