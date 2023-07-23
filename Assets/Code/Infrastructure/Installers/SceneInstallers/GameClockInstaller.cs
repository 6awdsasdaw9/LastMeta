using Code.Services.GameTime;
using Zenject;

namespace Code.Infrastructure.Installers.SceneInstallers
{
    public class GameClockInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameClock();
        }

        private void BindGameClock()
        {
            Container.BindInterfacesAndSelfTo<GameClock>().AsSingle().NonLazy();
        }
    }
}