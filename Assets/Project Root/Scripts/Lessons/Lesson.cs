using LessonsPractices;
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

            get => _nameOfLesson;
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


        public LessonPractice Practice { get => _practice; }



        private readonly string _nameOfLesson;

        private readonly string _iconName;

        private readonly LessonDifficulties _difficulty;

        private readonly LessonPractice _practice;


        private readonly List<LessonNode> _nodes;

        private readonly IList<LessonTheory> _theory;


        private readonly Dictionary<
            
            Languages, LessonTextContent> _content;


        private Sprite _sprite;

        private bool _isCompleted;


        public Lesson(LessonData data)
        {

            _nameOfLesson = data.NameOfLesson;

            _iconName = data.IconName;

            _nodes = data.Contents;


            _theory = data.Theory;

            _content = new Dictionary<Languages, 
                
                LessonTextContent>(_nodes.Count);
        }


        public Lesson(LessonObject data)
        {

            _nameOfLesson = data.NameOfLesson;

            _difficulty = data.Difficulty;

            _sprite = data.Icon;

            _practice = data.Practice;


            _content = new Dictionary<Languages, LessonTextContent>();
        }


        #region Load Icon

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


        public void LoadIcon(Sprite sprite)
        {
            _sprite = sprite;
        }

        #endregion


        #region Initialize Theory

        public void InitializeContent()
        {

            foreach(LessonNode node in _nodes)
            {

                _content.Add(node.Language, node.Texts);
            }
        }


        public void InitializeTheory()
        {

            foreach(LessonTheory theory in _theory)
            {

                if(Enum.TryParse(theory.Language, out Languages language))
                {

                    _content.TryAdd(language, new LessonTextContent(theory));
                }
            }
        }


        public void SetTheory(IList<LessonTheory> theories)
        {

            foreach (LessonTheory theory in theories)
            {

                if (Enum.TryParse(theory.Language, out Languages language))
                {

                    _content.TryAdd(language, new LessonTextContent(theory));
                }                
            }
        }

        #endregion


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