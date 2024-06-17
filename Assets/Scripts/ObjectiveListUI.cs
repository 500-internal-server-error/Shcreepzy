using TMPro;
using UnityEngine;

namespace Shcreepzy
{
    public class ObjectiveListUI : MonoBehaviour
    {
        public void PopObjective()
        {
            Object.Destroy(GetComponentInChildren<TextMeshProUGUI>().gameObject);
        }
    }
}
