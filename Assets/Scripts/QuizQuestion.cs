using System;
using UnityEngine;

namespace Shcreepzy
{
    [Serializable]
    public struct QuizQuestion
    {
        [field: SerializeField, TextArea(3, 3)] public string Question { get; private set; }

        [field: SerializeField, TextArea(3, 3), Space(30)] public string OptionA { get; private set; }
        [field: SerializeField] public bool OptionAIsCorrect { get; private set; }

        [field: SerializeField, TextArea(3, 3), Space(30)] public string OptionB { get; private set; }
        [field: SerializeField] public bool OptionBIsCorrect { get; private set; }

        [field: SerializeField, TextArea(3, 3), Space(30)] public string OptionC { get; private set; }
        [field: SerializeField] public bool OptionCIsCorrect { get; private set; }

        [field: SerializeField, TextArea(3, 3), Space(30)] public string OptionD { get; private set; }
        [field: SerializeField] public bool OptionDIsCorrect { get; private set; }
    }
}
