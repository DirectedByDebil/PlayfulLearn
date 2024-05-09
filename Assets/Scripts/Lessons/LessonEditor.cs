using UnityEngine;
using Localizations;
using Localizations.Lessons;
using Initializations;
using TMPro;
using System;
using System.Collections.Generic;
using UserInteraction.Switchers;

namespace Lessons
{
    public class LessonEditor : MonoBehaviour, IInitialization, ISwitchable
    {
        [SerializeField] private TMP_Dropdown _dropdown;
        [Space, SerializeField] private TMP_InputField _nameOfLesson;
        [SerializeField] private TMP_InputField _introductionText, _usageText, _descriptionText;

        private List<LessonNode> _lessonNodes = new();

        public event ISwitchable.SwitchHandler Switched;

        public void Initialize()
        {
            InitializeDropDown();

            Switched += gameObject.SetActive;
        }

        private void InitializeDropDown()
        {
            _dropdown.ClearOptions();

            foreach(var language in Enum.GetValues(typeof(Languages)))
            {
                TMP_Dropdown.OptionData option = new(language.ToString());
                _dropdown.options.Add(option);
            }
        }
        public void SaveLesson()
        {
            
        }
        public void OnSwitched(bool value)
        {
            Switched?.Invoke(value);
        }
    }
}