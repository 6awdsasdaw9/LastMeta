using System.Security.Cryptography.X509Certificates;
using Code.Data.Configs;
using Code.Logic.DayOfTime;
using Code.Services.SaveServices;
using Zenject;

namespace Code.Data.GameData
{
    public class GameSceneData
    {
        private readonly ScenesConfig _scenesConfig;

        public string CurrentScene { get; private set; }
        public SceneParams SceneParams { get; private set; }
        
      
        public GameSceneData(ScenesConfig scenesConfig)
        {
            _scenesConfig = scenesConfig;
        }
        
        public void Init(SavedData savedData)
        {
            CurrentScene = savedData.CurrentScene;
            SceneParams = _scenesConfig.GetSceneParam(CurrentScene);
        }
    }
}