using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace LessonsPractices
{
    public class PracticeSceneManager
    {

        public event Action<IMiniGame> MiniGameFound;

        public event Action<IBlocksPractice> BlocksPracticeFound;


        private PracticeType _lastPracticeType;

        private string _lastScene;


        public void LoadPracticeAsync(PracticeType practiceType, string sceneName)
        {

            UnLoadPracticeAsync();


            if (IsSceneLoaded(sceneName)) return;


            _lastPracticeType = practiceType;

            _lastScene = sceneName;


            AsyncOperation operation = SceneManager.LoadSceneAsync(_lastScene, LoadSceneMode.Additive);

            operation.allowSceneActivation = true;

            operation.completed += OnSceneLoaded;
        }


        public void UnLoadPracticeAsync()
        {

            if(_lastScene != null)
            {

                SceneManager.UnloadSceneAsync(_lastScene);

                _lastScene = null;
            }
        }
        

        private bool IsSceneLoaded(string sceneName)
        {

            Scene scene = SceneManager.GetSceneByName(sceneName);

            return scene.isLoaded;
        }


        private void OnSceneLoaded(AsyncOperation operation)
        {

            operation.completed -= OnSceneLoaded;

            FindPractice();
        }


        private void FindPractice()
        {

            Scene scene = SceneManager.GetSceneByName(_lastScene);

            GameObject[] objects = scene.GetRootGameObjects();

            
            foreach (GameObject obj in objects)
            {
                
                if(obj.TryGetComponent(out IPractice practice))
                {

                    InvokeFoundEvent(practice);

                    return;
                }
            }
        }


        private void InvokeFoundEvent(IPractice practice)
        {

            switch (_lastPracticeType)
            {

                case PracticeType.MiniGame:

                    if (practice is IMiniGame miniGame)
                    {
                        
                        MiniGameFound?.Invoke(miniGame);
                    }
                    break;


                case PracticeType.Blocks:

                    if(practice is IBlocksPractice blocks)
                    {

                        BlocksPracticeFound?.Invoke(blocks);
                    }
                    break;
            }
        }
    }
}
