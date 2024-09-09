using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UserInterface.Buttons
{
    [CreateAssetMenu(fileName = "Button Settings", 
        
        menuName = "Settings/ButtonElementSettings", order = 65)]


    public sealed class ButtonElementSettings : ScriptableObject
    {
        public Vector2 Size 
        {
            get => _size;
        }

        public Vector2 IconSize
        {
            get => _iconSize;
        }

        public Vector2 IconLocalPosition
        {
            get => _iconLocalPosition;
        }

        public Vector2 TextLocalPosition
        {
            get => _textLocalPosition;
        }


        public Sprite Background
        {
            get => _background;
        }

        public ColorBlock ColorBlock 
        {
            get => _colorBlock;
        }

        public TMP_FontAsset Font
        {
            get => _font;
        }

        public float FontSize 
        {
            get => _fontSize;
        }

        public FontStyles FontStyle
        {
            get => _fontStyle;
        }

        public TextAlignmentOptions TextAlignmentOptions 
        {
            get => _textAlignmentOptions;
        }


        [SerializeField] 
        
        private Vector2 _size;


        [SerializeField] 
        
        private Vector2 _iconSize;


        [SerializeField] 
        
        private Vector2 _iconLocalPosition;


        [SerializeField] 
        
        private Vector2 _textLocalPosition;

        

        [Space, SerializeField] 
        
        private Sprite _background;
        


        [Space, SerializeField]
        
        private ColorBlock _colorBlock;


        [Space, SerializeField]
        
        private TMP_FontAsset _font;

        

        [SerializeField, Range(1, 50)] 
        
        private float _fontSize;
        

        [SerializeField]
        
        private FontStyles _fontStyle;
        

        [SerializeField]
        
        private TextAlignmentOptions _textAlignmentOptions;
    }
}