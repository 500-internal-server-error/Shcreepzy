using UnityEngine;

namespace Shcreepzy
{
    public class LevelObjective : MonoBehaviour
    {
        [SerializeField] private bool hasMinimumTimeSinceLastObjective = false;
        [SerializeField, Min(0)] private float minimumTimeSinceLastObjective;

        public float? GetMinimumTimeSinceLastObjective()
        {
            if (hasMinimumTimeSinceLastObjective) return minimumTimeSinceLastObjective;
            return null;
        }
    }
}
