using UnityEngine;
using UnityEngine.EventSystems;
using UserInterface.Buttons;
using UserInteraction.Switchers;
using System;

namespace LearningPrograms
{
    public sealed class LearningProgramPresenter : MonoBehaviour,
        
        IPointerExitHandler, ISwitchable
    {

        public RectTransform ContentPosition
        {

            get => _placeForContent;
        }


        public event Action<LearningProgram> Loading;

        public event Action<bool> Switched;


        [SerializeField] private ButtonElementSettings _buttonElementSettings;

        [Space, SerializeField] private RectTransform _placeForContent;

        [SerializeField, Range(10, 300)] private int _dx,_dy;



        public void SetTableOfPrograms(TableOfLearningPrograms tableOfPrograms)
        {

            if (tableOfPrograms)
            {

                DrawPrograms(tableOfPrograms, 0);

                Loading?.Invoke(tableOfPrograms._lastLearningProgram);
            }
        }


        public void RefreshTableOfPrograms(TableOfLearningPrograms tableOfPrograms)
        {

            if(tableOfPrograms)
            {

                int index = transform.childCount-1;

                DrawPrograms(tableOfPrograms, index);
            }
        }


        private void DrawPrograms(TableOfLearningPrograms tableOfPrograms, int startIndex)
        {

            var programs = tableOfPrograms.LearningPrograms;


            for(int i = startIndex; i < programs.Count; i++)
            {

                var program = programs[i];

                Vector2 localPosition = new(_buttonElementSettings.Size.x/2 + _dx, -i * (_buttonElementSettings.Size.y + _dy));


                void action()
                {
                    tableOfPrograms._lastLearningProgram = program;

                    Loading?.Invoke(program);
                }


                //var button = _buttonDrawer.DrawButton(program, localPosition);

                //button.Clicked += action;
            }
        }


        public void OnSwitched(bool value)
        {

            Switched?.Invoke(value);
        }


        public void OnPointerExit(PointerEventData eventData)
        {

            if (eventData.position.x > 0)
            {

                Switched?.Invoke(false);
            }
        }   
    }
}