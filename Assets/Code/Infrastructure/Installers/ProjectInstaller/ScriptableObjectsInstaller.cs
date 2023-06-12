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
        public GameSettings GameSettings;
        public HeroConfig HeroConfig;
        public PrefabsData PrefabsData;
        public TextConfig TextConfig;
        public SceneAudioPath SceneAudioPath;
        public HudSettings HudSettings;
        public SpawnPointsConfig SpawnPointsConfig;
        public override void InstallBindings()
        {
            Container.BindInstance(ScenesConfig);
            Container.BindInstance(GameSettings);
            Container.BindInstance(HeroConfig);
            Container.BindInstance(PrefabsData);
            Container.BindInstance(TextConfig);
            Container.BindInstance(SceneAudioPath);
            Container.BindInstance(HudSettings);
            Container.BindInstance(SpawnPointsConfig);
        }
    }
}