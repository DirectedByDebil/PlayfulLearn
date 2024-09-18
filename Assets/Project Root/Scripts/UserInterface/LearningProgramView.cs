using UnityEngine;
using TMPro;
using LearningPrograms;

//#TODO put this to learning programs namespace
namespace UserInterface
{
    [RequireComponent(typeof(RectTransform))]

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
                
            _currentProgramNameText.text = learningProgram.name;


            _progressBar.UpdateProgressBar(learningProgram);
        }
    }
}