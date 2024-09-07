using UnityEngine;
using LearningPrograms;
using TMPro;

namespace UserInterface
{
    public sealed class ProgressBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _progressText;

        [SerializeField, Header("Max width of progress bar"), Range(200, 800) ] private float _maxWidth;
        [SerializeField] private RectTransform _progressBar;

        public void UpdateProgressBar(LearningProgram learningProgram)
        {
            float currentProgress = 0f;

            foreach(var lesson in learningProgram.Lessons)
            {
                if (lesson.IsCompleted)
                    currentProgress++;
            }

            currentProgress /= learningProgram.Lessons.Count;
           
            _progressText.text = string.Format("Completed: {0} %", Mathf.RoundToInt(currentProgress*100));

            _progressBar.sizeDelta = new Vector2(currentProgress * _maxWidth, _progressBar.rect.height);
        }
    }
}