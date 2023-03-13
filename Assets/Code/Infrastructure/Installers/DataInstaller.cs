using Code.Data.GameData;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "DataInstaller", menuName = "ScriptableObjects/GameSettings/DataInstaller")]
    public class DataInstaller: ScriptableObjectInstaller<DataInstaller>
    {
        public SettingsData settingsData;
        public ConfigData configData;
        public PrefabsData prefabsData;

        public override void InstallBindings()
        {
            Container.BindInstance(settingsData);
            Container.BindInstance(configData);
            Container.BindInstance(prefabsData);
        }
    }
}