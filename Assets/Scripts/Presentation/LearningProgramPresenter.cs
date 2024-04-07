using UnityEngine;
using UnityEngine.EventSystems;
using LearningProgramSystem;

namespace Presentation
{
    public sealed class LearningProgramPresenter : MonoBehaviour, IPointerExitHandler
    {
        public LearningProgram LastChoosedProgram { get { return _tableOfPrograms._lastLearningProgram; } }

        public delegate void PresentProgramHandler(LearningProgram program);
        public event PresentProgramHandler OnProgramPresented;

        [SerializeField] private TableOfLearningPrograms _tableOfPrograms;

        [Space, SerializeField] private RectTransform _placeForContent;

        private readonly Vector2 _slotSize = new (200, 100);
        private const int _dy = -150;

        private void Start()
        {
            DrawPrograms();

            gameObject.SetActive(false);
        }

        private void DrawPrograms()
        {
            int index = 0;
            foreach(var program in _tableOfPrograms.LearningPrograms)
            {
                DrawLearningProgramButton(program, index);
                index++;
            }
        }
        private void DrawLearningProgramButton(LearningProgram learningProgram, int index)
        {
            LearningProgramButton button = new(learningProgram, _slotSize);
            button.rectTransform.SetParent(_placeForContent, true);
            button.rectTransform.localPosition = new Vector2(_slotSize.x, (index + 1) * _dy);

            button.onClickEvent += () =>
            {
                _tableOfPrograms._lastLearningProgram = learningProgram;
                OnProgramPresented?.Invoke(learningProgram);
            };
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(eventData.position.x > 0)
              gameObject.SetActive(false);
        }   
    }
}