using UnityEngine;
using Slider = UnityEngine.UI.Slider;
using TMPro;

namespace Practice
{
    [System.Serializable]
    public struct Rigidbody2DSettings
    {
        public delegate void SliderEvent (float value);
        public event SliderEvent GravityChangedEvent, DragChangedEvent, AngularDragChangedEvent;

        [SerializeField] private Slider _gravitySlider,
            _dragSlider, _angularDragSlider;
        [Space, SerializeField] private TextMeshProUGUI _gravityText;
        [SerializeField] private TextMeshProUGUI _dragText, _angularText;

        public void Initialization()
        {
            SubscribeSliders();

            OnGravityValueChanged(_gravitySlider.value);
            OnDragValueChanged(_dragSlider.value);
            OnAngularDragValueChanged(_angularDragSlider.value);
        }

        private void SubscribeSliders()
        {
            _gravitySlider.onValueChanged.AddListener(OnGravityValueChanged);
            _dragSlider.onValueChanged.AddListener(OnDragValueChanged);
            _angularDragSlider.onValueChanged.AddListener(OnAngularDragValueChanged);
        }

        private void OnGravityValueChanged(float gravityValue)
        {
            _gravityText.text = string.Format("Gravity: {0}", gravityValue);
            GravityChangedEvent?.Invoke(gravityValue);
        }
        private void OnDragValueChanged(float dragValue)
        {
            _dragText.text = string.Format("Drag: {0}", dragValue);
            DragChangedEvent?.Invoke(dragValue);
        }
        private void OnAngularDragValueChanged(float angularDragValue)
        {
            _angularText.text = string.Format("Angular drag: {0}", angularDragValue);
            AngularDragChangedEvent?.Invoke(angularDragValue);
        }
    }
}