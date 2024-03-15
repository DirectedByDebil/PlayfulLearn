using UnityEngine;
using UnityEngine.UI;
using TMPro;

public sealed class LessonButton
{
    public event UnityEngine.Events.UnityAction onClickEvent;
    public RectTransform rectTransform { get; private set; }
    
    private Lesson _lesson;
    private GameObject _obj, _child;
    private Button _button;

    private Vector2 _size;

    public LessonButton(Lesson lesson, Vector2 size)
    {
        _lesson = lesson;
        _size = size;

        _obj = new GameObject(_lesson.NameOfLesson);
        _child = new GameObject(_lesson.NameOfLesson + " Text");

        _button = _obj.AddComponent<Button>();
        _button.onClick.AddListener(() => onClickEvent?.Invoke());

        SetSizes();
        SetImage();
        SetText();
        SetParent();
    }

    private void SetSizes()
    {
        rectTransform =_obj.AddComponent<RectTransform>();
        rectTransform.sizeDelta = _size;
        rectTransform.anchorMin = new Vector2(0f, 0.5f);
        rectTransform.anchorMax = new Vector2(0f, 0.5f);
    }
    private void SetParent()
    {
        var rect = _child.GetComponent<RectTransform>();
        rect.localPosition = Vector3.zero;
        rect.SetParent(rectTransform, true);        
    }
    private void SetImage()
    {
        Image image = _obj.AddComponent<Image>();
        image.sprite = _lesson.BackgroundSprite;
        image.preserveAspect = true;

        _button.targetGraphic = image;
    }
    private void SetText()
    {
        TextMeshProUGUI text = _child.AddComponent<TextMeshProUGUI>();
        text.text = _lesson.NameOfLesson;
        text.rectTransform.sizeDelta = _size;
        text.alignment = TextAlignmentOptions.Center;
        text.color = Color.black;
        text.fontSize = 36;
    }
}