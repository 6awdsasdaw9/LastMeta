using Code.Logic.DayOfTime;
using Zenject;

namespace Code.Infrastructure.Installers.SceneInstallers
{
    public class TimeOfDayInstaller: MonoInstaller
    {
        public override void InstallBindings() => 
            BindTimeOfDayController();

        private void BindTimeOfDayController() =>
            Container.BindInterfacesAndSelfTo<TimeOfDayController>().AsSingle().NonLazy();
    }
}