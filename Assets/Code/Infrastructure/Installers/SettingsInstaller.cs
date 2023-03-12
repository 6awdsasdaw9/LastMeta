using Code.Data;
using Code.Data.GameData;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "SettingsInstaller", menuName = "ScriptableObjects/GameSettings/SettingsInstaller")]
    public class SettingsInstaller: ScriptableObjectInstaller<SettingsInstaller>
    {
        public GameSettings settings;
        public GameConfig config;

        public override void InstallBindings()
        {
            Container.BindInstance(settings);
            Container.BindInstance(config);
        }
    }
}