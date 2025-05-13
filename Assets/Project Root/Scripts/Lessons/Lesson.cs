using Localization;
using Extensions;
using Core;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace Lessons
{
    public sealed class Lesson : IComparable<Lesson>
    {

        public string NameOfLesson
        {

            get => _nameOfLeson;
        }

        public LessonDifficulties Difficulty { get => _difficulty; }

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

        private readonly LessonDifficulties _difficulty;

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


            Enum.TryParse(data.Difficulty, out _difficulty); 

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

        public int CompareTo(Lesson other)
        {
            return Difficulty.CompareTo(other.Difficulty);
        }
    }
}