using UnityEngine;
using Button = UnityEngine.UI.Button;
using Lessons;

namespace UserInteraction.LessonPracticeSystem
{
    [RequireComponent(typeof(Button))]
    public class FinishLessonButton : MonoBehaviour
    {
        [SerializeField] private Lesson _lesson;
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();

            _button.onClick.AddListener(delegate
            {
                _lesson.SetCompleted();
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
            });
        }
    }
}