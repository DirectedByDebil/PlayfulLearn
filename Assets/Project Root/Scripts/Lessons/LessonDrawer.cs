using UnityEngine;
using TMPro;
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


        private NewLesson _currentLesson;
        

        public void RenderLesson(NewLesson lesson)
        {

            //#TODO read from session data
            Languages language = Languages.Russian;

            
            _currentLesson = lesson;

            UpdateTexts(language);
            

            _contentRectTransform.localPosition = new Vector2
                
                (0, _contentRectTransform.localPosition.y);
            

            gameObject.SetActive(true);
        }


        private void UpdateTexts(Languages language)
        {

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