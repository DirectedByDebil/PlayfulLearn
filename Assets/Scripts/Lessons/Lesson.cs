using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Localizations.Lessons;
using Localizations;
using Initializations;

namespace Lessons
{
    [CreateAssetMenu(fileName = "New Lesson", menuName = "Lesson", order = 60)]
    public class Lesson : ScriptableObject, IInitialization
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

        public void Initialize()
        {
            foreach (var node in _content)
            {
                Content.TryAdd(node.Language, node.Texts);
            }
        }

        public void SetCompleted()
        {
            _isCompleted = true;
        }
        public void SetLesson(LessonTemplate template)
        {
            _nameOfLesson = template.nameOfLesson;
            Content = template.content;
            _practiceScene = template.practiceScene;

            foreach(Languages language in Content.Keys)
            {
                _content.Add(new LessonNode(language, Content[language]));
            }

            _isCompleted = false;
        }
    }
}