using UnityEngine;
using TMPro;
using UserInterface;

namespace LearningPrograms
{
    [RequireComponent(typeof(RectTransform))]

    //#TODO inherit from UIObject
    public sealed class LearningProgramView : MonoBehaviour
    {

        [Space, SerializeField]
        
        private TextMeshProUGUI _currentProgramNameText;


        [Space, SerializeField]
        
        private ProgressBar _progressBar;


        [Space, SerializeField] 
        
        private RectTransform _contentPosition;


        public void ViewLearningProgram(LearningProgram learningProgram)
        {

            _contentPosition.localPosition = new Vector3(0, 0);
                
            _currentProgramNameText.text = learningProgram.NameOfProgram;


            _progressBar.UpdateProgressBar(learningProgram);
        }
    }
}