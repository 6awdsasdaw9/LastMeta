    using Code.Character;
using Code.Character.Hero;
using Code.Data.GameData;
using Code.Data.ProgressData;
using Code.Logic.DayOfTime;
using Code.Services.Input;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class SceneInstaller : MonoInstaller,IInitializable
    {
        public override void InstallBindings()
        {
            BindSaveData();
            BindTimeOfDayController();
            
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
            Container.BindInterfacesTo<SceneInstaller>()
                .FromInstance(this);

        private void BindSaveData() => 
            Container.Bind<SavedDataCollection>().AsSingle().NonLazy();

        private void BindInput() =>
            Container.Bind<InputController>()
                .AsSingle()
                .NonLazy();

        private void BindMovementLimiter() =>
            Container.Bind<MovementLimiter>()
                .AsSingle()
                .NonLazy();

        private void BindTimeOfDayController() => 
            Container.BindInterfacesAndSelfTo<TimeOfDayController>().AsSingle().NonLazy();

        private void BindHero()
        {
            var prefabsData = Container.Resolve<PrefabsData>();
            
            HeroMovement heroPrefab = SceneManager.GetActiveScene().name == Constants.homeScene ? prefabsData.realHero : prefabsData.hero;
            Vector3 initialPoint = GameObject.FindGameObjectWithTag(Constants.InitialPointTag).transform.position;

            HeroMovement hero = Container.InstantiatePrefabForComponent<HeroMovement>(
                heroPrefab,
                initialPoint,
                Quaternion.identity,
                null);
            
            Container.Bind<HeroMovement>().FromInstance(hero).AsSingle().NonLazy();
        }
        private void LoadGameProgress()
        {
            PersistentSavedDataService dataService = Container.Resolve<PersistentSavedDataService>();
            dataService.SetSavedDataCollection(Container.Resolve<SavedDataCollection>());
            dataService.LoadProgress();
        }
    }
}
