using UnityEngine;
using TMPro;
using Lessons;
using UnityEngine.UI;

namespace UserInterface
{
    [RequireComponent(typeof(Toggle))]
    public sealed class LessonToggle: MonoBehaviour
    {

        public Lesson Lesson
        {

            get;

            private set;
        }


        public bool IsOn 
        {

            get => _toggle.isOn;
        }


        [SerializeField]
        
        private TextMeshProUGUI _label;
        

        private Toggle _toggle;


        private void OnValidate()
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