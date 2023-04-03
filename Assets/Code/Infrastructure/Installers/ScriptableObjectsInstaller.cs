using Code.Data.GameData;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "ScriptableObjectsInstaller", menuName = "ScriptableObjects/GameSettings/ScriptableObjectsInstaller")]
    public class ScriptableObjectsInstaller: ScriptableObjectInstaller<ScriptableObjectsInstaller>
    {
        public SettingsData settingsData;
        public ConfigData configData;
        public PrefabsData prefabsData;
        public TextData textData;
        public override void InstallBindings()
        {
            Container.BindInstance(settingsData);
            Container.BindInstance(configData);
            Container.BindInstance(prefabsData);
            Container.BindInstance(textData);
        }
    }
}