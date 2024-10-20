using UnityEngine;
using System.Collections.Generic;
using Localization;
using Extensions;
using Core;

namespace Lessons
{
    public sealed class Lesson
    {

        public string NameOfLesson
        {

            get => _nameOfLeson;
        }


        public bool IsCompleted
        {

            get => _isCompleted;
        }


        public Sprite Icon
        {

            get => _sprite;
        }


        public IReadOnlyDictionary<
            Languages, LessonTextContent> Content
        {

            get => _content;
        }



        private readonly string _nameOfLeson;

        private readonly string _iconName;

        private readonly List<LessonNode> _nodes;


        private readonly Dictionary<
            
            Languages, LessonTextContent> _content;


        private Sprite _sprite;

        private bool _isCompleted;


        public Lesson(LessonData data)
        {

            _nameOfLeson = data.NameOfLesson;

            _iconName = data.IconName;

            _nodes = data.Contents;


            _content = new Dictionary<Languages, 
                
                LessonTextContent>(_nodes.Count);
        }


        public void LoadIcon()
        {

            string fileName = string.Format("{0}/{1}",

                PathKeeper.LessonsIconPath, _iconName);
            

            if(ImageExtensions.TryCreateSprite(fileName,
                
                out Sprite sprite))
            {

                _sprite = sprite;
            }
        }


        public void InitializeContent()
        {

            foreach(LessonNode node in _nodes)
            {

                _content.Add(node.Language, node.Texts);
            }
        }


        public void SetCompleted(bool isCompleted)
        {

            _isCompleted = isCompleted;
        }
    }
}