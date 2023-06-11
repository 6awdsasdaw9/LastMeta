using Code.PresentationModel;
using Code.Services;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers.ProjectInstaller
{
    public class SceneLoaderInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtain _curtain;
        public override void InstallBindings()
        {
            BindSceneLoader();
            BindLoadingCurtain();
        }
        
        private void BindSceneLoader() =>
            Container.Bind<SceneLoader>()
                .AsSingle()
                .NonLazy();

        private void BindLoadingCurtain() =>
            Container.Bind<LoadingCurtain>().FromInstance(_curtain).AsSingle().NonLazy();

    }
}