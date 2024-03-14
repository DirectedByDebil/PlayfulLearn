using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[RequireComponent(typeof(RectTransform))]
public sealed class Menu : MonoBehaviour
{
    [SerializeField] private TableOfLessons _table;
    [SerializeField] private LessonDrawer _lessonDrawer;
    
    [Space, SerializeField] private RectTransform _topPanel;

    [Space, SerializeField] private Transform _contentPosition;

    private RectTransform _rectTransform;

    private const int LESSONS_IN_ROW = 5,
        SPRITE_WIDTH = 300, SPRITE_HEIGHT = 300;
    private int LESSONS_IN_COLUMN;

    private List<Vector3> _vectors = new List<Vector3>();
    private int _amount;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();

        CountLessonsInColumn();
        CountVectors();

        DrawLessons();

        _contentPosition.position = new Vector3(0, _topPanel.sizeDelta.y);
    }

    private void CountLessonsInColumn()
    {
        _amount = _table.Lessons.Count;

        LESSONS_IN_COLUMN = _amount / LESSONS_IN_ROW;

        if (LESSONS_IN_COLUMN == 0)
            LESSONS_IN_COLUMN = 1;

        if (_amount % LESSONS_IN_ROW > 0)
            LESSONS_IN_COLUMN++;
    }
    private void CountVectors()
    {
        float xPadding = (_rectTransform.rect.width - SPRITE_WIDTH * LESSONS_IN_ROW) / (LESSONS_IN_ROW + 1);
        for (int i = 0; i < _amount; i++)
        {
            _vectors.Add(new Vector3(
                xPadding * (i % LESSONS_IN_ROW + 1) + (i % LESSONS_IN_ROW + 0.5f) * SPRITE_WIDTH,
                - (i / LESSONS_IN_ROW + 0.5f) * SPRITE_HEIGHT - _topPanel.sizeDelta.y,
                0));
        }
    }
    private void DrawLessons()
    {
        int index = 0;

        foreach (var lesson in _table.Lessons)
        {
            DrawLessonButton(lesson, index);
            index++;
        }
    }
    private void DrawLessonButton(Lesson lesson, int index)
    {
        LessonButton lessonButton = new (lesson, new Vector2(SPRITE_WIDTH, SPRITE_HEIGHT));

        lessonButton.rectTransform.SetParent(_contentPosition, true);
        lessonButton.rectTransform.localPosition = _vectors[index];
        
        lessonButton.onClickEvent += delegate
        {
            _lessonDrawer.CurrentLesson = lesson;
            _lessonDrawer.RenderLesson();
            gameObject.SetActive(false);
        };
    }
}