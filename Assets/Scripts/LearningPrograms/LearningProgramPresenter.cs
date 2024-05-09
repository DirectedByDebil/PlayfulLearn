using UnityEngine;
using UnityEngine.EventSystems;
using UserInterface.Buttons;
using Initializations;
using UserInteraction.Switchers;
using System;

namespace LearningPrograms
{
    public sealed class LearningProgramPresenter : MonoBehaviour, IPointerExitHandler, IInitialization, ISwitchable
    {
        public LearningProgram LastChoosedProgram { get { return _tableOfPrograms._lastLearningProgram; } }

        public delegate void PresentProgramHandler(LearningProgram program);
        public event PresentProgramHandler OnProgramPresented;
        public event ISwitchable.SwitchHandler Switched;

        [SerializeField] private TableOfLearningPrograms _tableOfPrograms;
        
        [Space, SerializeField] private RectTransform _placeForContent;

        private ButtonDrawer _buttonDrawer;

        private readonly Vector2 _slotSize = new (200, 100);
        private const int _dy = -150;

        public void Initialize()
        {
            gameObject.SetActive(true);
            _buttonDrawer = new ButtonDrawer(_slotSize, _placeForContent);
            DrawPrograms();

            Switched += gameObject.SetActive;
            gameObject.SetActive(false);
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

        public void LoadLastProgram()
        {
            if (LastChoosedProgram)
                OnProgramPresented?.Invoke(LastChoosedProgram);
        }
        public void OnSwitched(bool value)
        {
            Switched?.Invoke(value);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.position.x > 0)
                Switched?.Invoke(false);
        }   
    }
}