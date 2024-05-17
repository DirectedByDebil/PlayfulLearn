using UnityEngine;
using Localizations;
using Initializations;
using TMPro;
using System;
using System.Collections.Generic;
using UserInteraction.Switchers;
using UnityEditor;

namespace Lessons
{
    public class LessonEditor : MonoBehaviour, IInitialization, ISwitchable
    {
        [SerializeField] private TMP_Dropdown _dropdown;
        [Space, SerializeField] private TMP_InputField _nameOfLesson;
        [SerializeField] private TMP_InputField _introductionText, _usageText, _descriptionText;
        [Space, SerializeField] private SceneAsset scene;

        private Dictionary<Languages, LessonTextContent> _lessonContent = new();
        private LessonTemplate _lessonTemplate;
        private Languages _lastLanguage;

        public event ISwitchable.SwitchHandler Switched;
        public delegate void LessonEditorHandler(Lesson lesson);
        public event LessonEditorHandler LessonAdded;

        public void Initialize()
        {
            InitializeDropDown();

            Switched += gameObject.SetActive;
        }

        private void InitializeDropDown()
        {
            _dropdown.ClearOptions();

            foreach(Languages language in Enum.GetValues(typeof(Languages)))
            {
                TMP_Dropdown.OptionData option = new(language.ToString());
                _dropdown.options.Add(option);

                _lessonContent.TryAdd(language, default);
            }
            
            _dropdown.onValueChanged.AddListener(SaveContent);
        }
        private void SaveContent(int index)
        {
            Languages language = (Languages)index;

            LessonTextContent content = new(_introductionText.text,
                _usageText.text, _descriptionText.text);

            _lessonContent[_lastLanguage] = content;
            _lastLanguage = language;

            LessonTextContent savedContent = _lessonContent[_lastLanguage];
            _introductionText.text = savedContent.Introduction;
            _usageText.text = savedContent.Usage;
            _descriptionText.text = savedContent.PracticeDescription;
        }

        public void SaveLesson()
        {
            SaveContent(_dropdown.value);

            string path = string.Format("Assets/Resources/Lessons"),
                nameOfLesson = _nameOfLesson.text,
                assetName = string.Format("{0}/{1}/{1}.asset", path, nameOfLesson);

            Lesson asset = ScriptableObject.CreateInstance<Lesson>();

            _lessonTemplate.nameOfLesson = nameOfLesson;
            _lessonTemplate.content = _lessonContent;
            //#TODO import scene from form
            _lessonTemplate.practiceScene = scene;
            asset.SetLesson(_lessonTemplate);

            AssetDatabase.CreateFolder(path, nameOfLesson);
            AssetDatabase.CreateAsset(asset, assetName);
            
            Switched?.Invoke(false);
            LessonAdded?.Invoke(asset);
        }
        public void OnSwitched(bool value)
        {
            Switched?.Invoke(value);
        }
    }
}