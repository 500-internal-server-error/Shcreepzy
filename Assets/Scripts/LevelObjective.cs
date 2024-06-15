using UnityEngine;

namespace Shcreepzy
{
    public class LevelObjective : MonoBehaviour
    {
        [SerializeField] private bool hasMinimumTimeSinceLastObjective = false;
        [SerializeField, Min(0)] private float minimumTimeSinceLastObjective;
        [SerializeField] private bool popsObjective = true;

        public float? GetMinimumTimeSinceLastObjective()
        {
            if (hasMinimumTimeSinceLastObjective) return minimumTimeSinceLastObjective;
            return null;
        }

        public bool ShouldPopObjective()
        {
            return popsObjective;
        }
    }
}
