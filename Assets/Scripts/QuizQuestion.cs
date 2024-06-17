using System;
using UnityEngine;

namespace Shcreepzy
{
    [Serializable]
    public struct QuizQuestion
    {
        [Header("Question")]
        [field: SerializeField] public bool QuestionHasImage { get; private set; }
        [field: SerializeField, TextArea(3, 3)] public string QuestionText { get; private set; }
        [field: SerializeField] public Sprite QuestionImage { get; private set; }

        [Header("Option A")]
        [field: SerializeField, Space(30)] public QuizOptionType OptionAType { get; private set; }
        [field: SerializeField, TextArea(3, 3)] public string OptionAText { get; private set; }
        [field: SerializeField] public Sprite OptionAImage { get; private set; }
        [field: SerializeField] public bool OptionAIsCorrect { get; private set; }

        [Header("Option B")]
        [field: SerializeField, Space(30)] public QuizOptionType OptionBType { get; private set; }
        [field: SerializeField, TextArea(3, 3)] public string OptionBText { get; private set; }
        [field: SerializeField] public Sprite OptionBImage { get; private set; }
        [field: SerializeField] public bool OptionBIsCorrect { get; private set; }

        [Header("Option C")]
        [field: SerializeField, Space(30)] public QuizOptionType OptionCType { get; private set; }
        [field: SerializeField, TextArea(3, 3)] public string OptionCText { get; private set; }
        [field: SerializeField] public Sprite OptionCImage { get; private set; }
        [field: SerializeField] public bool OptionCIsCorrect { get; private set; }

        [Header("Option D")]
        [field: SerializeField, Space(30)] public QuizOptionType OptionDType { get; private set; }
        [field: SerializeField, TextArea(3, 3)] public string OptionDText { get; private set; }
        [field: SerializeField] public Sprite OptionDImage { get; private set; }
        [field: SerializeField] public bool OptionDIsCorrect { get; private set; }
    }
}
