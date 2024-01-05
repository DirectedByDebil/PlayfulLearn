using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(RectTransform))]
public class LessonDrawer : MonoBehaviour
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
                _sceneToLoad = CurrentLesson.Practice.Scene.name;

                SetTexts();
            }
        }
    }

    [SerializeField]
    private TMP_Text _header,
        _introduction, _usage, _practiceDescription;
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Image _background;

    [Space, SerializeField] private Sprite _standartSprite;

    [Space, SerializeField] private Color _standartColor, _colorForPractice;

    private RectTransform _rectTransform;
    private Lesson _currentLesson;
    private Image[] _images = new Image[3];

    private string _sceneToLoad;

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

    private void SetTexts()
    {
        _header.text = CurrentLesson.NameOfLesson;
        _introduction.text = CurrentLesson.Theory.Introduction;
        _usage.text = CurrentLesson.Theory.Usage;
        _practiceDescription.text = CurrentLesson.Practice.Description;
    }
    private void SetLessonGraphics()
    {
        _background.sprite = CurrentLesson.Theory.Sprite;
    }
    private void SetStandartGraphics()
    {
        _background.sprite = _standartSprite;
    }

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();

        _images[0] = _introduction.GetComponentInParent<Image>();
        _images[1] = _usage.GetComponentInParent<Image>();
        _images[2] = _practiceDescription.GetComponentInParent<Image>();

        foreach(var image in _images)
        {
            if (image != _images[2])
                image.color = _standartColor;
            else
                image.color = _colorForPractice;
        }
    }
}