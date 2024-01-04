using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[RequireComponent(typeof(RectTransform))]
public class Menu : MonoBehaviour
{
    [SerializeField] private TableOfLessons _table;
    [SerializeField] private LessonDrawer _lessonDrawer;
    [SerializeField] private RectTransform _topPanel;

    private RectTransform _rectTransform;

    private const int LESSONS_IN_ROW = 5,
        SPRITE_WIDTH = 300, SPRITE_HEIGHT = 300;
    private int LESSONS_IN_COLUMN;

    private List<Vector3> _vectors = new List<Vector3>();
    private int _amount;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();

        _amount = _table.Lessons.Count;

        LESSONS_IN_COLUMN = _amount / LESSONS_IN_ROW;

        if (LESSONS_IN_COLUMN == 0)
            LESSONS_IN_COLUMN = 1;

        if (_amount % LESSONS_IN_ROW > 0)
            LESSONS_IN_COLUMN++;

        CountVectors();
        DrawLessons();
    }

    private void DrawLessons()
    {
        int index = 0;

        foreach (var lesson in _table.Lessons)
        {
            SetLesson(lesson, index);
            index++;
        }
    }
    private void CountVectors()
    {
        float xPadding = (_rectTransform.rect.width - SPRITE_WIDTH * LESSONS_IN_ROW) / (LESSONS_IN_ROW + 1);
        for (int i = 0; i < _amount; i++)
        {
            _vectors.Add(new Vector3(
                xPadding * (i % LESSONS_IN_ROW + 1) + (i % LESSONS_IN_ROW + 0.5f) * SPRITE_WIDTH,
                - (i / LESSONS_IN_ROW + 0.5f) * SPRITE_HEIGHT + _rectTransform.rect.height / 2
                - _topPanel.sizeDelta.y,
                0));
        }
    }
    private void SetLesson(Lesson lesson, int index)
    {
        GameObject obj = new(lesson.NameOfLesson);
        GameObject child = new(lesson.NameOfLesson + " Text");

        Image image = obj.AddComponent<Image>();
        image.sprite = lesson.Theory.Sprite;
        image.preserveAspect = true;

        Button button = obj.AddComponent<Button>();
        button.targetGraphic = image;
        button.onClick.AddListener(() => ShowLesson(lesson));

        TextMeshProUGUI text = child.AddComponent<TextMeshProUGUI>();
        text.text = lesson.NameOfLesson;
        text.rectTransform.sizeDelta = new Vector2(SPRITE_WIDTH, SPRITE_HEIGHT);
        text.alignment = TextAlignmentOptions.Center;
        text.color = Color.black;
        text.fontSize = SPRITE_HEIGHT/10;

        child.transform.SetParent(obj.transform, true);
        obj.transform.SetParent(transform, true);

        obj.transform.localPosition = _vectors[index];
        child.GetComponent<RectTransform>().localPosition = Vector3.zero;

        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(SPRITE_WIDTH, SPRITE_HEIGHT);
        rect.anchorMin = new Vector2(0f, 0.5f);
        rect.anchorMax = new Vector2(0f, 0.5f);
    }
    private void ShowLesson(Lesson lesson)
    {
        _lessonDrawer.CurrentLesson = lesson;
        _lessonDrawer.RenderLesson();
        gameObject.SetActive(false);
    }
}