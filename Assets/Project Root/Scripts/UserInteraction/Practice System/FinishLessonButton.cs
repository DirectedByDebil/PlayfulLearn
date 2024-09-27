using UnityEngine;
using UnityEngine.UI;

namespace UserInteraction.LessonPracticeSystem
{

    [RequireComponent(typeof(Button))]

    public class FinishLessonButton : MonoBehaviour
    {

        private Button _button;


        private void OnValidate()
        {
            
            _button = GetComponent<Button>();
        }


        private void Start()
        {

            _button.onClick.AddListener(delegate
            {

                //_lesson.SetCompleted();
                
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
            });
        }
    }
}