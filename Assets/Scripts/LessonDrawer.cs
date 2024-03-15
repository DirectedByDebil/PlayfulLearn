using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(RectTransform))]
public sealed class LessonDrawer : MonoBehaviour
{
    public Lesson CurrentLesson
    {
        private get
        { return _currentLesson; }
        set
        {
            if (value != _currentLesson)
            {
                _currentLesson = value;

                _rectTransform.localPosition = new Vector3(0, _rectTransform.localPosition.y);
                _sceneToLoad = CurrentLesson.PracticeScene.name;

                SetLanguages();
                UpdateTexts(_languagesDropDown.value);
            }
        }
    }

    [SerializeField]
    private TMP_Text _header,
        _introduction, _usage, _practiceDescription;
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Image _background;
    [SerializeField] private TMP_Dropdown _languagesDropDown;

    [Space, SerializeField] private Sprite _standartSprite;

    [Space, SerializeField] private Color _standartColor, _colorForPractice;

    private RectTransform _rectTransform;
    private Lesson _currentLesson;
    private Image[] _images = new Image[3];

    private string _sceneToLoad;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        
        SetImages();
        
        _languagesDropDown.onValueChanged.AddListener(  (int x) => { UpdateTexts(x); } );
    }
    private void SetImages()
    {
        _images[0] = _introduction.GetComponentInParent<Image>();
        _images[1] = _usage.GetComponentInParent<Image>();
        _images[2] = _practiceDescription.GetComponentInParent<Image>();

        foreach (var image in _images)
        {
            if (image != _images[2])
                image.color = _standartColor;
            else
                image.color = _colorForPractice;
        }
    }
    private void UpdateTexts(int index)
    {
        _header.text = CurrentLesson.NameOfLesson;

        LessonTextContent content = CurrentLesson.Content[index].Texts;

        _introduction.text = content.Introduction;
        _usage.text = content.Usage;
        _practiceDescription.text = content.PracticeDescription;
    }

    private void SetLanguages()
    {
        _languagesDropDown.options.Clear();
        
        foreach(var key in CurrentLesson.Content)
        {
            var item = new TMP_Dropdown.OptionData(key.Language.ToString());
            _languagesDropDown.options.Add(item);
        }
    }

    public void LoadLesson()
    {
        SceneManager.LoadSceneAsync(_sceneToLoad);
    }
    public void RenderLesson()
    {
        if (_toggle.isOn)
            SetLessonGraphics();
        else
            SetStandartGraphics();
    }

    private void SetLessonGraphics()
    {
        _background.sprite = CurrentLesson.BackgroundSprite;
    }
    private void SetStandartGraphics()
    {
        _background.sprite = _standartSprite;
    }
}