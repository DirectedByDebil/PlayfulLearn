using UnityEngine;
using LessonSystem;

namespace LearningProgramSystem
{
    [System.Serializable]
    public struct LearningProgramDescription
    {
        public Languages Language { get { return _language; } }
        public string Description { get { return _description; } }

        [SerializeField] private Languages _language;
        [SerializeField, TextArea(5,20)] private string _description;
    }
}