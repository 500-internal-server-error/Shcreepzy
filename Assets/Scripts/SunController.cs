using UnityEngine;

namespace Shcreepzy
{
    public class SunController : MonoBehaviour
    {
        [SerializeField] private Light source;

        // 0.02 s/t = 50 t/s = 15000 t / 5 m
        private const int MAX_TIME = 15000;
        [SerializeField, Range(0, MAX_TIME)] private int time;

        [SerializeField] private bool doDaylightCycle;

        private void FixedUpdate()
        {
            if (doDaylightCycle) time++;
            if (time >= MAX_TIME) time = 0;
            source.transform.rotation = Quaternion.Euler(50, (float)time / (float)MAX_TIME * 360, 0);
        }
    }
}
