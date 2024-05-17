using UnityEngine;
using TMPro;
using Lessons;
using UnityEngine.UI;
using Initializations;

namespace UserInterface
{
    [RequireComponent(typeof(Toggle))]
    public sealed class LessonToggle: MonoBehaviour, IInitialization
    {
        public Lesson Lesson { get; private set; }
        public bool IsOn { get { return _toggle.isOn; } }

        [SerializeField] private TextMeshProUGUI _label;
        private Toggle _toggle;
        
        public void Initialize()
        {
            _toggle = GetComponent<Toggle>();
            _toggle.isOn = false;
        }

        public void SetLesson(Lesson lesson)
        {
            Lesson = lesson;
            _label.text = Lesson.NameOfLesson;
        }
    }
}