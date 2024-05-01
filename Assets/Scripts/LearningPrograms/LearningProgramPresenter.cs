using UnityEngine;
using UnityEngine.EventSystems;
using UserInterface.Buttons;

namespace LearningPrograms
{
    public sealed class LearningProgramPresenter : MonoBehaviour, IPointerExitHandler
    {
        public LearningProgram LastChoosedProgram { get { return _tableOfPrograms._lastLearningProgram; } }

        public delegate void PresentProgramHandler(LearningProgram program);
        public event PresentProgramHandler OnProgramPresented;

        [SerializeField] private TableOfLearningPrograms _tableOfPrograms;
        [SerializeField] private LearningProgram _allLessons;

        [Space, SerializeField] private RectTransform _placeForContent;

        private ButtonDrawer _buttonDrawer;

        private readonly Vector2 _slotSize = new (200, 100);
        private const int _dy = -150;

        public void Initialize()
        {
            InitializeLessons();

            _buttonDrawer = new ButtonDrawer(_slotSize, _placeForContent);
            DrawPrograms();

            if (LastChoosedProgram)
                OnProgramPresented?.Invoke(LastChoosedProgram);

            gameObject.SetActive(false);
        }

        private void InitializeLessons()
        {
            foreach(var lesson in _allLessons.Lessons)
            {
                lesson.Initialize();
            }
        }
        private void DrawPrograms()
        {
            int index = 0;
            foreach(var program in _tableOfPrograms.LearningPrograms)
            {
                Vector2 localPosition = new (_slotSize.x, (index + 1) * _dy);

                void action()
                {
                    _tableOfPrograms._lastLearningProgram = program;
                    OnProgramPresented?.Invoke(program);
                }

                var button = _buttonDrawer.DrawButton(program, localPosition);
                button.Clicked += action;

                index++;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(eventData.position.x > 0)
              gameObject.SetActive(false);
        }   
    }
}