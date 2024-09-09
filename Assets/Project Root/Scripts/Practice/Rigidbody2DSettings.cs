using UnityEngine;
using Slider = UnityEngine.UI.Slider;
using TMPro;
using System;

namespace Practice
{
    [Serializable]
    public struct Rigidbody2DSettings
    {

        public event Action<float> GravityChangedEvent;

        public event Action<float> DragChangedEvent;
        
        public event Action<float> AngularDragChangedEvent;


        [SerializeField] private Slider _gravitySlider;

        [SerializeField] private Slider _dragSlider;

        [SerializeField] private Slider _angularDragSlider;


        [Space, SerializeField]
        
        private TextMeshProUGUI _gravityText;


        [SerializeField]
        
        private TextMeshProUGUI _dragText;


        [SerializeField]
        
        private TextMeshProUGUI _angularText;


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