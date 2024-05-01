using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Localizations;

namespace Lessons
{
    public sealed class LessonDrawer : MonoBehaviour
    {
        [SerializeField] private RectTransform _contentRectTransform;

        [Space, SerializeField] private TMP_Text _header,
            _introduction, _usage, _practiceDescription;

        private Lesson _currentLesson;
        private Languages _currentLanguage;

        private string _sceneToLoad;

        private void Start()
        {
            gameObject.SetActive(false);
        }
     
        public void LoadLesson()
        {
            SceneManager.LoadSceneAsync(_sceneToLoad);
        }
        public void RenderLesson(Lesson lesson, Languages language)
        {
            if (lesson != _currentLesson)
            {
                _currentLesson = lesson;
                
                _sceneToLoad = _currentLesson.PracticeScene.name;

                UpdateTexts(language);
            }
            else if(language != _currentLanguage)
            {
                UpdateTexts(language);
            }
            
            _contentRectTransform.localPosition = new Vector2(0, _contentRectTransform.localPosition.y);
            gameObject.SetActive(true);
        }

        private void UpdateTexts(Languages language)
        {
            _currentLanguage = language;

            _header.text = _currentLesson.NameOfLesson;
            string introText, usageText, practiceText;

            if(_currentLesson.Content.ContainsKey(language) )
            {
                LessonTextContent content = _currentLesson.Content[language];

                introText = content.Introduction;
                usageText = content.Usage;
                practiceText = content.PracticeDescription;
            }
            else
            {
                introText = usageText = practiceText = "No such language(";
            }

            _introduction.text = introText;
            _usage.text = usageText;
            _practiceDescription.text = practiceText;
        }
    }
}