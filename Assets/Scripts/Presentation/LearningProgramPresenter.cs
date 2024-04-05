using UnityEngine;
using UnityEngine.EventSystems;
using LearningProgramSystem;

namespace Presentation
{
    public class LearningProgramPresenter : MonoBehaviour, IPointerExitHandler
    {
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
        private void DrawLearningProgramButton(LearningProgram program, int index)
        {
            LearningProgramButton button = new(program, _slotSize);
            button.rectTransform.SetParent(_placeForContent, true);
            button.rectTransform.localPosition = new Vector2(_slotSize.x, (index + 1) * _dy);

            button.onClickEvent += delegate { };
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(eventData.position.x > 0)
              gameObject.SetActive(false);
        }   
    }
}