using Code.Audio;
using Code.Data.Configs;
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
        public SceneAudioPath SceneAudioPath;
        public HudSettings HudSettings;
        public override void InstallBindings()
        {
            Container.BindInstance(GameSettings);
            Container.BindInstance(GameConfig);
            Container.BindInstance(PrefabsData);
            Container.BindInstance(TextConfig);
            Container.BindInstance(SceneAudioPath);
            Container.BindInstance(HudSettings);
        }
    }
}