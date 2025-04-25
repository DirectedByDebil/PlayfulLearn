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

            _lastScene = sceneName;


            AsyncOperation operation = SceneManager.LoadSceneAsync(_lastScene, LoadSceneMode.Additive);

            operation.allowSceneActivation = true;

            operation.completed += OnSceneLoaded;
        }


        public void UnLoadMiniGameAsync()
        {

            SceneManager.UnloadSceneAsync(_lastScene);
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
