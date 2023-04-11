using Code.Data.Configs;
using Code.Data.GameData;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "ScriptableObjectsInstaller", menuName = "ScriptableObjects/GameSettings/ScriptableObjectsInstaller")]
    public class ScriptableObjectsInstaller: ScriptableObjectInstaller<ScriptableObjectsInstaller>
    {
        public GameSettings GameSettings;
        public GameConfig GameConfig;
        public PrefabsData PrefabsData;
        public TextConfig TextConfig;
        public override void InstallBindings()
        {
            Container.BindInstance(GameSettings);
            Container.BindInstance(GameConfig);
            Container.BindInstance(PrefabsData);
            Container.BindInstance(TextConfig);
        }
    }
}