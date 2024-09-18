using UnityEngine;
using Localization;
using TMPro;
using System.Collections.Generic;
using UserInterface;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Lessons
{
    public sealed class LessonEditorView : MonoBehaviour,

        IEditor
    {

        public Button.ButtonClickedEvent CreateClicked
        {

            get => _createButton.onClick;
        }


        public Button.ButtonClickedEvent CloseClicked
        {

            get => _closeButton.onClick;
        }


        #region Buttons

        [SerializeField, Space]

        private Button _createButton;


        [SerializeField, Space]

        private Button _closeButton;

        #endregion


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


        public void SaveLesson()
        {
            //SaveContent(_dropdown.value);

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

            //LessonAdded?.Invoke(asset);
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
    }
}