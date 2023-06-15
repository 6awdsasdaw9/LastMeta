using Code.Audio.AudioPath;
using Code.Data.Configs;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers.ProjectInstaller
{
    [CreateAssetMenu(fileName = "ScriptableObjectsInstaller", menuName = "ScriptableObjects/ScriptableObjectsInstaller")]
    public class ScriptableObjectsInstaller: ScriptableObjectInstaller<ScriptableObjectsInstaller>
    {
        public ScenesConfig ScenesConfig;
        public HeroConfig HeroConfig;
        public AssetsConfig assetsConfig;
        public TextConfig TextConfig;
        public SceneAudioPath SceneAudioPath;
        public HudSettings HudSettings;
        public override void InstallBindings()
        {
            Container.BindInstance(ScenesConfig);
            Container.BindInstance(HeroConfig);
            Container.BindInstance(assetsConfig);
            Container.BindInstance(TextConfig);
            Container.BindInstance(SceneAudioPath);
            Container.BindInstance(HudSettings);
        }
    }
}