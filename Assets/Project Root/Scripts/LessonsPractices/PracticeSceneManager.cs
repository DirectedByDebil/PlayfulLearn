using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace LessonsPractices
{
    public class PracticeSceneManager
    {

        public event Action<IMiniGame> MiniGameFound;


        private string _lastScene;


        public void LoadMiniGameAsync(string sceneName)
        {

            UnLoadMiniGameAsync();


            if (IsSceneLoaded(sceneName)) return;

            _lastScene = sceneName;


            AsyncOperation operation = SceneManager.LoadSceneAsync(_lastScene, LoadSceneMode.Additive);

            operation.allowSceneActivation = true;

            operation.completed += OnSceneLoaded;
        }


        public void UnLoadMiniGameAsync()
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

            FindMiniGame();
        }


        private void FindMiniGame()
        {

            Scene scene = SceneManager.GetSceneByName(_lastScene);

            GameObject[] objects = scene.GetRootGameObjects();

            
            foreach (GameObject obj in objects)
            {
                
                if(obj.TryGetComponent(out IMiniGame miniGame))
                {

                    MiniGameFound?.Invoke(miniGame);

                    return;
                }
            }
        }
    }
}
