using Web;
using Extensions;
using Lessons;
using LearningPrograms;
using Playables;
using UINew;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace Core
{
    [RequireComponent(typeof(UIDocument))]
    public sealed class InitScene : MonoBehaviour
    {

        [DllImport("__Internal")]
        private static extern void CheckJs();


        [SerializeField, Space]
        private List<Character> _characters;


        [SerializeField, Space]
        private string _menuSceneName;


        private string _lastProgramName;


        private UIDocument _document;

        private Label _outPut;


        private void Awake()
        {

            //CheckJs();

            _document = GetComponent<UIDocument>();

            _outPut = _document.GetLabel("output");


            _outPut.text = "Initializing user";


            InitUser();


            
            _outPut.text = "Initializing characters";

            InitCharacters();


            _outPut.text = "Initializing lessons";
            InitializeLessons();


            _outPut.text = "Initializing last learning program";
            InitializeLastProgram();

            _outPut.text = "Initializing learning programs";
            InitializeLearningProgramsAsync();


            SceneManager.LoadSceneAsync(_menuSceneName);
        }


        private void InitUser()
        {

            UserData user = FileExtensions.GetFromFile<UserData>(PathKeeper.UserDataPath);

            SessionData.SetUserData(user);


            InitSelectedCharacter(user.SelectedCharacter);
        }


        private void InitSelectedCharacter(string selectedType)
        {

            Character selectedCharacter;


            if(Enum.TryParse(selectedType, false, out Characters type))
            {

                selectedCharacter = _characters.Find(character => character.CharacterType == type);
            }
            else
            {
                selectedCharacter = _characters[0];
            }


            SessionData.SetCharacter(selectedCharacter);
        }


        private void InitCharacters()
        {

            SessionData.Characters = _characters;
        }


        private void InitializeLessons()
        {

            List<string> completed = SessionData.UserData.CompletedLessons;


            _outPut.text = "Started lessons";

            _outPut.text = PathKeeper.LessonsPath;

            IReadOnlyCollection<LessonData> allLessonsData =
                
                FileExtensions.GetFiles<LessonData>(PathKeeper.LessonsPath);

            _outPut.text = allLessonsData.Count.ToString();

            List<Lesson> allLessons = new(allLessonsData.Count);


            foreach(LessonData data in allLessonsData)
            {

                _outPut.text = data.NameOfLesson;

                Lesson lesson = new (data);

                lesson.InitializeContent();

                lesson.LoadIcon();


                if(completed.Contains(lesson.NameOfLesson))
                {

                    lesson.SetCompleted(true);

                    completed.Remove(lesson.NameOfLesson);
                }


                allLessons.Add(lesson);
            }

            allLessons.Sort();

            SessionData.SetAllLessons(allLessons);
        }


        private void InitializeLastProgram()
        {

            if(FileExtensions.TryReadFile(
                
                PathKeeper.LastLearningProgramName,
                
                out string result))
            {

                _lastProgramName = result;
            }
        }


        private async Task InitializeLearningProgramsAsync()
        {

            IReadOnlyCollection<LearningProgramData> allProgramsData =

                FileExtensions.GetFiles<LearningProgramData>(PathKeeper.LearningProgramsPath);


            List<LearningProgram> allPrograms = new(allProgramsData.Count);


            foreach (LearningProgramData data in allProgramsData)
            {

                LearningProgram program = new(data);

                program.LoadIcon();

                program.AddLessonsAsync(SessionData.AllLessons);

                program.CountProgressAsync();


                allPrograms.Add(program);
            }


            allPrograms.Sort();


            SessionData.SetAllLearningPrograms(allPrograms);



            LearningProgram last =
                
                allPrograms.Find(FindLastLearningProgram);


            SessionData.SetLastLearningProgram(last);
        }


        private bool FindLastLearningProgram(LearningProgram learningProgram)
        {

            return learningProgram.NameOfProgram == _lastProgramName;
        }
    }
}