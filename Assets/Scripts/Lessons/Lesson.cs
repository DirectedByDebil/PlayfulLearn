using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Localizations.Lessons;
using Localizations;

namespace Lessons
{
    [CreateAssetMenu(fileName = "New Lesson", menuName = "Lesson", order = 60)]
    public class Lesson : ScriptableObject
    {
        public string NameOfLesson { get { return _nameOfLesson; } }
        public Sprite BackgroundSprite { get { return _backgroundSprite; } }
        public SceneAsset PracticeScene { get { return _practiceScene; } }
        public bool IsCompleted { get { return _isCompleted; } }

        public Dictionary<Languages, LessonTextContent> Content { get; private set; } = new();

        [SerializeField] private string _nameOfLesson;
        [Space, SerializeField] private Sprite _backgroundSprite;
        [Space, SerializeField] private SceneAsset _practiceScene;
        [Space, SerializeField] private List<LessonNode> _content = new();
        [SerializeField] private bool _isCompleted;

        public void SetCompleted()
        {
            _isCompleted = true;
        }

        public void Initialize()
        {
            foreach (var node in _content)
            {
                Content.TryAdd(node.Language, node.Texts);
            }
        }
    }
}