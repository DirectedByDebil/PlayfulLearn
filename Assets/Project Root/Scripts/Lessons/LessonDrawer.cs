using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Localization;

namespace Lessons
{
    public sealed class LessonDrawer : MonoBehaviour
    {

        [SerializeField] private RectTransform _contentRectTransform;


        #region Texts Handlers
        
        [Space, SerializeField] private TMP_Text _header;

        [Space, SerializeField] private TMP_Text _introduction;

        [Space, SerializeField] private TMP_Text _usage;

        [Space, SerializeField] private TMP_Text _practiceDescription;

        #endregion


        private Lesson _currentLesson;
        
        private Languages _currentLanguage;

        private string _sceneToLoad;
     

        public void LoadLesson()
        {

            SceneManager.LoadSceneAsync(_sceneToLoad);
        }


        public void RenderLesson(Lesson lesson)
        {

            //#TODO read from session data
            Languages language = Languages.English;

            if (lesson != _currentLesson)
            {

                _currentLesson = lesson;
                
                _sceneToLoad = _currentLesson.PracticeScene;

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