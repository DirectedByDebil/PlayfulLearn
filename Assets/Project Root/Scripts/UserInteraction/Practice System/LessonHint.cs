using UnityEngine;
using UnityEngine.EventSystems;
using Button = UnityEngine.UI.Button;

namespace UserInteraction.LessonPracticeSystem
{

    [RequireComponent(typeof(Button))]

    public class LessonHint : MonoBehaviour,
        
        IPointerEnterHandler, IPointerExitHandler
    {

        [SerializeField] 
        
        private GameObject _hintObject;


        private Button _button;


        private bool _isSelected;


        public void OnPointerEnter(PointerEventData eventData)
        {

            if(!_isSelected)
            {

                _hintObject.SetActive(true);
            }
        }


        public void OnPointerExit(PointerEventData eventData)
        {

            if(!_isSelected)
            {

                _hintObject.SetActive(false);
            }
        }


        private void OnValidate()
        {
            
            _button = GetComponent<Button>();
        }


        private void Start()
        {

            _isSelected = false;


            _button.onClick.AddListener(delegate 
            {

                _isSelected = !_isSelected;

                _hintObject.SetActive(_isSelected);
            });
        }
    }
}