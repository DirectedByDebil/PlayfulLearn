using Web;
using Lessons;
using LearningPrograms;
using Playables;
using UINew;
using Extensions;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Core
{
    [RequireComponent(typeof(UIDocument))]
    public sealed class InitScene : MonoBehaviour
    {

        [SerializeField, Space]
        private List<Character> _characters;

        
        [SerializeField, Space]
        private List<LessonObject> _lessonInitData;


        [SerializeField, Space]
        private List<LearningProgramObject> _learningProgramsInitData;
        

        [SerializeField, Space]
        private string _menuSceneName;



        private UIDocument _document;

        private Label _outPut;
        
        /*
        private void Awake()
        {

            Init();

            PathKeeper.SetRoot("Assets/Project Root/");

            FileExtensions.TryReadFile(PathKeeper.InitDataPath, out string json);

            LoadInitData(json);
        }*/



        public void Init()
        {

            _document = GetComponent<UIDocument>();
            
            _outPut = _document.GetLabel("output");


            _lessonInitData.Sort();

            _learningProgramsInitData.Sort();


            _outPut.text = "Initializing";

            InitCharacters();
        }


        public void LoadInitData(string initDataJson)
        {

            _outPut.text = "Loading Init Data";


            InitData init = JsonUtility.FromJson<InitData>(initDataJson);


            InitUser(init.User);

            InitLessons(init.Lessons, init.User.CompletedLessons);

            InitializeLearningProgramsAsync(init.LearningPrograms);

            InitStartingLearningProgram(init.StartingModule);


            _outPut.text = "Loading Scene";
            
            SceneManager.LoadSceneAsync(_menuSceneName);
        }


        private void InitCharacters()
        {

            SessionData.Characters = _characters;
        }


        #region Init User

        private void InitUser(UserData user)
        {

            _outPut.text = "Initializing user";


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

        #endregion

        
        private void InitLessons(IReadOnlyCollection<LessonData> logicData,
            IList<string> completed)
        {

            _outPut.text = "Initializing lessons";
            

            List<Lesson> allLessons = new(logicData.Count);
           
            
            foreach (LessonData logic in logicData)
            {

                LessonObject initData = _lessonInitData.Find(lessonData => lessonData.NameOfLesson == logic.NameOfLesson);


                if (initData == null) continue;



                _lessonInitData.Remove(initData);


                Lesson lesson = new(initData);

                lesson.SetTheory(logic.Theory);


                if (completed.Contains(lesson.NameOfLesson))
                {

                    lesson.SetCompleted(true);

                    completed.Remove(lesson.NameOfLesson);
                }


                allLessons.Add(lesson);
            }


            allLessons.Sort();

            SessionData.SetAllLessons(allLessons);
        }


        
        private async Task InitializeLearningProgramsAsync(IReadOnlyCollection<LearningProgramData> logicData)
        {

            _outPut.text = "Initializing Learning Modules";


            List<LearningProgram> allPrograms = new(logicData.Count);


            foreach (LearningProgramData data in logicData)
            {

                int index = _learningProgramsInitData.FindIndex(obj => obj.NameOfProgram == data.NameOfProgram);

                LearningProgram program = new(_learningProgramsInitData[index], index + 1);


                program.AddLessons(data.Lessons, SessionData.AllLessons);

                program.CountProgressAsync();


                allPrograms.Add(program);
            }

            allPrograms.Sort();


            SessionData.SetAllLearningPrograms(allPrograms);
        }
        

        private void InitStartingLearningProgram(string startingName)
        {

            LearningProgram startingModule = SessionData.AllLearningPrograms.Find(program => program.NameOfProgram == startingName);


            if(startingModule == null)
            {

                startingModule = SessionData.AllLearningPrograms[0];
            }
            
            SessionData.SetLastLearningProgram(startingModule);
        }
    }
}