using Code.Data;
using Code.Data.Configs;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "SettingsInstaller", menuName = "ScriptableObjects/GameSettings/SettingsInstaller")]
    public class SettingsInstaller: ScriptableObjectInstaller<SettingsInstaller>
    {
        public GameSettings settings;

        public override void InstallBindings()
        {
            Container.BindInstance(settings);
        }
    }
}