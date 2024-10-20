using UnityEngine;
using LearningPrograms;
using Lessons;
using TMPro;
using System.Collections.Generic;

namespace UserInterface
{
    public sealed class ProgressBar : MonoBehaviour
    {

        [SerializeField] 
        
        private TextMeshProUGUI _progressText;


        [SerializeField, Header("Max width of progress bar"),
            
            Range(200, 800) ]
        
        private float _maxWidth;


        [SerializeField]
        
        private RectTransform _progressBar;


        public void UpdateProgressBar(LearningProgram learningProgram)
        {

            float currentProgress = CountProgress(learningProgram.Lessons);
           

            _progressText.text = string.Format(
                
                "Completed: {0} %", Mathf.RoundToInt(currentProgress*100));

            //#TODO do it with filled image
            _progressBar.sizeDelta = new Vector2(
                
                currentProgress * _maxWidth,
                
                _progressBar.rect.height);
        }


        private float CountProgress(IReadOnlyCollection<Lesson> lessons)
        {

            float currentProgress = 0f;


            foreach (Lesson lesson in lessons)
            {

                if (lesson.IsCompleted)
                {

                    currentProgress++;
                }
            }


            currentProgress /= lessons.Count;


            return currentProgress;
        }
    }
}