using System.Collections.Generic;
using UnityEngine;
using Localization;

namespace Lessons
{
    [CreateAssetMenu(fileName = "New Lesson",
        
        menuName = "Lesson", order = 60)]
    
    public class Lesson : ScriptableObject
    {

        public string NameOfLesson
        {

            get => _nameOfLesson; 
        }


        public Sprite Icon
        {

            get =>_icon;
        }


        public string PracticeScene
        {
            
            get => _practiceScene;
        }
        

        public bool IsCompleted
        {

            get => _isCompleted;
        }


        public Dictionary<Languages, LessonTextContent> Content
        { 

            get;

            private set;
        }


        [SerializeField] private string _nameOfLesson;

        [Space, SerializeField] private Sprite _icon;

        [Space, SerializeField] private string _practiceScene;

        [Space, SerializeField] private List<LessonNode> _content;

        [SerializeField] private bool _isCompleted;


        private void OnValidate()
        {

            Content =
                
                new Dictionary<Languages, LessonTextContent>(_content.Count);


            foreach (LessonNode node in _content)
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