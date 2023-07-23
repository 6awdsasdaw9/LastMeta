using System;
using System.Collections;
using System.Threading;
using Code.Infrastructure;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Services
{
    public class SceneLoader
    {
        public void LoadAsync(string name, Action onLoaded = null) =>
            LoadSceneAsync(name, onLoaded).Forget();

        public void Load(string name)
        {
            SceneManager.LoadScene(name);
        }
        
        private async UniTaskVoid LoadSceneAsync(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke(); 
                return;
            }
            
            var cancellationToken = new CancellationTokenSource();
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: cancellationToken.Token);
            
            onLoaded?.Invoke();
        }
        
        
        
    }
} 