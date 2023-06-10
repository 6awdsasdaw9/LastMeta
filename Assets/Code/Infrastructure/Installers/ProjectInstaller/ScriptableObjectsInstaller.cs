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
        public HeroConfig heroConfig;
        public PrefabsData PrefabsData;
        public TextConfig TextConfig;
        public SceneAudioPath SceneAudioPath;
        public HudSettings HudSettings;
        public SpawnPointsConfig SpawnPointsConfig;
        public override void InstallBindings()
        {
            Container.BindInstance(GameSettings);
            Container.BindInstance(heroConfig);
            Container.BindInstance(PrefabsData);
            Container.BindInstance(TextConfig);
            Container.BindInstance(SceneAudioPath);
            Container.BindInstance(HudSettings);
            Container.BindInstance(SpawnPointsConfig);
        }
    }
}