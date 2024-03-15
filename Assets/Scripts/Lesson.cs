using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Lesson", menuName = "NewLesson", order = 60)]
public class Lesson : ScriptableObject
{
    public string NameOfLesson { get { return _nameOfLesson; } }
    public Categories Category { get { return _category; } }
    public Sprite BackgroundSprite { get { return _backgroundSprite; } }
    public UnityEditor.SceneAsset PracticeScene { get { return _practiceScene; } }

    public List<LessonNode> Content { get { return _content; } }


    [SerializeField] private string _nameOfLesson;
    [SerializeField] private Categories _category;
    [Space, SerializeField] private Sprite _backgroundSprite;
    [Space, SerializeField] private UnityEditor.SceneAsset _practiceScene;
    [Space, SerializeField] private List<LessonNode> _content = new();
}