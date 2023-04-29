using Code.Character.Hero;
using Code.Data.Configs;
using Code.Data.GameData;
using Code.Data.ProgressData;
using Code.Logic.DayOfTime;
using Code.Services;
using Code.Services.Input;
using Code.UI;
using Code.UI.HeadUpDisplay;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class GameSceneInstaller : MonoInstaller,IInitializable
    {
        public PrefabsData prefabsData;
        public override void InstallBindings()
        {
            BindSaveData();
            BindTimeOfDayController();
            
            BindHud();
            BindMovementLimiter();
            BindInput();
            BindHero();
            BindInterfaces();
        }


        public void Initialize()
        {
            LoadGameProgress();
        }

        private void BindInterfaces() =>
            Container.BindInterfacesTo<GameSceneInstaller>()
                .FromInstance(this);

        private void BindSaveData() => 
            Container.Bind<SavedDataCollection>().AsSingle().NonLazy();

        private void BindInput() =>
            Container.Bind<InputService>()
                .AsSingle()
                .NonLazy();

        private void BindMovementLimiter() =>
            Container.Bind<MovementLimiter>()
                .AsSingle()
                .NonLazy();

        private void BindTimeOfDayController() => 
            Container.BindInterfacesAndSelfTo<TimeOfDayController>().AsSingle().NonLazy();

        private void BindHud()
        {
            
            HUD hud = Container.InstantiatePrefabForComponent<HUD>(
                prefabsData.GameHUD,
                Vector3.zero, 
                Quaternion.identity,
                null);
            Container.Bind<HUD>().FromInstance(hud).AsSingle().NonLazy();
        }
        
        private void BindHero()
        {
            Hero heroPrefab = prefabsData.HeroPrefab;
            Vector3 initialPoint = GameObject.FindGameObjectWithTag(Constants.InitialPointTag).transform.position;

            IHero hero = Container.InstantiatePrefabForComponent<IHero>(
                heroPrefab,
                initialPoint,
                Quaternion.identity,
                null);
            
            Container.Bind<IHero>().FromInstance(hero).AsSingle().NonLazy();
        }
        
        private void LoadGameProgress()
        {
            PersistentSavedDataService dataService = Container.Resolve<PersistentSavedDataService>();
            dataService.SetSavedDataCollection(Container.Resolve<SavedDataCollection>());
            dataService.LoadProgress();
        }
    }
}
