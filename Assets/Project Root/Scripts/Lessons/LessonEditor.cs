using UnityEngine;
using Localization;
using TMPro;
using System;
using System.Collections.Generic;
using UserInteraction.Switchers;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Lessons
{
    public sealed class LessonEditor : MonoBehaviour, ISwitchable
    {

        [SerializeField] private TMP_Dropdown _dropdown;


        #region Input Field Handlers

        [Space, SerializeField] private TMP_InputField _nameOfLesson;

        [SerializeField] private TMP_InputField _introductionText;

        [SerializeField] private TMP_InputField _usageText;

        [SerializeField] private TMP_InputField _descriptionText;

        #endregion


        [Space, SerializeField] private string _scene;


        private Dictionary<Languages, LessonTextContent> _lessonContent = new();

        private LessonTemplate _lessonTemplate;

        private Languages _lastLanguage;


        public event Action<bool> Switched;

        public delegate void LessonEditorHandler(Lesson lesson);

        public event LessonEditorHandler LessonAdded;


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
            _lessonTemplate.practiceScene = _scene;
            asset.SetLesson(_lessonTemplate);

            #if UNITY_EDITOR
            AssetDatabase.CreateFolder(path, nameOfLesson);
            AssetDatabase.CreateAsset(asset, assetName);
            #endif

            Switched?.Invoke(false);
            LessonAdded?.Invoke(asset);
        }

        public void OnSwitched(bool value)
        {

            Switched?.Invoke(value);
        }
    }
}