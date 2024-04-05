using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LessonSystem
{
    [CreateAssetMenu(fileName = "New Lesson", menuName = "Lesson", order = 60)]
    public class Lesson : ScriptableObject
    {
        public string NameOfLesson { get { return _nameOfLesson; } }
        public Categories Category { get { return _category; } }
        public Sprite BackgroundSprite { get { return _backgroundSprite; } }
        public SceneAsset PracticeScene { get { return _practiceScene; } }
        public bool IsCompleted { get { return _isCompleted; } }

        public List<LessonNode> Content { get { return _content; } }

        [SerializeField] private string _nameOfLesson;
        [SerializeField] private Categories _category;
        [Space, SerializeField] private Sprite _backgroundSprite;
        [Space, SerializeField] private SceneAsset _practiceScene;
        [Space, SerializeField] private List<LessonNode> _content = new();
        [SerializeField] private bool _isCompleted;

        public void SetCompeted()
        {
            _isCompleted = true;
        }
    }
}