using Code.Data.Configs;
using Code.Data.Configs.HeroConfigs;
using Code.Data.Configs.TextsConfigs;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers.ProjectInstaller
{
    [CreateAssetMenu(fileName = "ScriptableObjectsInstaller", menuName = "ScriptableObjects/ScriptableObjectsInstaller")]
    public class ScriptableObjectsInstaller: ScriptableObjectInstaller<ScriptableObjectsInstaller>
    {
        public ScenesConfig ScenesConfig;
        public AssetsConfig AssetsConfig;
        public HeroConfig HeroConfig;
        public HudSettings HudSettings;

        [Space] 
        public EnemiesConfig EnemiesConfig;
        public ItemsConfig ItemsConfig;
        public ObjectsConfig ObjectsConfig;
        
        [Space, Title("Localization")]
        public TextConfigEng TextConfig_Eng;
        public TextConfigRus TextConfig_Rus;
        
        public override void InstallBindings()
        {
            Container.BindInstance(ScenesConfig);
            Container.BindInstance(HeroConfig);
            Container.BindInstance(AssetsConfig);
            Container.BindInstance(HudSettings);
            
            Container.BindInstance(ItemsConfig);
            Container.BindInstance(ObjectsConfig);
            Container.BindInstance(EnemiesConfig);
            
            Container.BindInstance(TextConfig_Eng);
            Container.BindInstance(TextConfig_Rus);
        }
    }
}