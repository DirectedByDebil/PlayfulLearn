using System.Collections.Generic;
using Localization;
using Extensions;

namespace Lessons
{
    public sealed class LessonEditorModel
    {

        private Dictionary<Languages, LessonTextContent> _content;


        public LessonEditorModel(IReadOnlyCollection<Languages> languages)
        {

            _content = new Dictionary<
                
                Languages, LessonTextContent>(languages.Count);
            

            foreach(Languages language in languages)
            {

                _content.TryAdd(language, new LessonTextContent());
            }
        }


        public LessonTextContent GetContent(Languages language)
        {

            return _content[language];
        }


        public void UpdateContent(Languages language, 
            
            LessonTextContent content)
        {

            _content[language] = content;
        }


        public void SaveLesson(string nameOfLesson)
        {

            string iconName = "IconName.png";


            LessonData data = new(nameOfLesson,

                iconName,

                ContentToNodes());


            string fileName = string.Format(

                "Assets/Project Root/Lessons/{0}.json",
                
                nameOfLesson);


            FileExtensions.WriteJson(data, fileName);
        }


        private List<LessonNode> ContentToNodes()
        {

            List<LessonNode> nodes = new (_content.Count);


            foreach(Languages language in _content.Keys)
            {

                nodes.Add(new LessonNode(
                    
                    language, _content[language]));
            }

            return nodes;
        }
    }
}
