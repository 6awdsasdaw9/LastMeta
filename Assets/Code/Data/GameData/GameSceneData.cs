using System.Security.Cryptography.X509Certificates;
using Code.Data.Configs;
using Code.Logic.DayOfTime;
using Code.Services.SaveServices;
using Zenject;

namespace Code.Data.GameData
{
    public class GameSceneData
    {
        public readonly ScenesConfig ScenesConfig;
        public string CurrentScene { get; private set; }
        public SceneParams CurrentSceneParams { get; private set; }
        
      
        public GameSceneData(ScenesConfig scenesConfig)
        {
            ScenesConfig = scenesConfig;
        }
        
        public void Init(SavedData savedData)
        {
            CurrentScene = savedData.CurrentScene;
            CurrentSceneParams = ScenesConfig.GetSceneParam(CurrentScene);
        }
    }
}