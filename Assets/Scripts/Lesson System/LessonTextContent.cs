using UnityEngine;

namespace LessonSystem
{
    [System.Serializable]
    public struct LessonTextContent
    {
        public string Introduction { get { return _introduction; } }
        public string Usage { get { return _usage; } }
        public string PracticeDescription { get { return _practiceDescription; } }

        [TextArea(5, 20), SerializeField] private string _introduction, _usage, _practiceDescription;
    }
}