using UnityEngine;
using Initializations;
using UserInteraction.Switchers;
using System.Collections.Generic;
using UserInterface;
using Lessons;
using TMPro;
using System.Linq;
using UnityEditor;

namespace LearningPrograms
{
    public class LearningProgramEditor : MonoBehaviour, IInitialization, ISwitchable
    {
        public delegate void ProgramCreatedHandler(LearningProgram program);
        public event ProgramCreatedHandler LearningProgramCreated;

        public event ISwitchable.SwitchHandler Switched;

        [SerializeField] private LessonToggle _template;
        [Space, SerializeField] private TMP_InputField _nameOfProgramText;

        private List<LessonToggle> _lessonToggles;

        public void Initialize()
        {
            Switched += gameObject.SetActive;
        }

        public void DrawLessonToggles(List<Lesson> lessons)
        {
            _lessonToggles = new(lessons.Count);

            float xPosition = _template.transform.localPosition.x,
                yPosition = _template.transform.localPosition.y,
                height = ((RectTransform)_template.transform).rect.height;

            _template.Initialize();
            _template.SetLesson(lessons[0]);
            _lessonToggles.Add(_template);

            for(int i = 1; i < lessons.Count; i++)
            {
                LessonToggle instance = Instantiate(_template, _template.transform.parent);
                instance.Initialize();
                instance.SetLesson(lessons[i]);
                _lessonToggles.Add(instance);

                yPosition -= height;
                instance.transform.localPosition = new Vector2(xPosition, yPosition);
            }
        }

        public void OnSwitched(bool value)
        {
            Switched?.Invoke(value);
        }

        public void SaveLearningProgram()
        {
            string path = "Assets/Resources/LearningPrograms",
                nameOfProgram = _nameOfProgramText.text,
                assetName = string.Format("{0}/{1}.asset", path, nameOfProgram);

            LearningProgram asset = ScriptableObject.CreateInstance<LearningProgram>();

            var selectedLessons = from toggle in _lessonToggles
                                  where toggle.IsOn
                                  select toggle.Lesson;

            List<Lesson> lessons = new();
            foreach (var lesson in selectedLessons)
                lessons.Add(lesson);

            LearningProgramTemplate template = new(lessons);
            asset.SetProgram(template);

            AssetDatabase.CreateAsset(asset, assetName);

            LearningProgramCreated?.Invoke(asset);
            Switched?.Invoke(false);
        }
    }
}