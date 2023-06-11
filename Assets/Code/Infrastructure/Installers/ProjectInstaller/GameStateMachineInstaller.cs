using Code.Infrastructure.StateMachine;
using Code.Infrastructure.StateMachine.States;
using Zenject;

namespace Code.Infrastructure.Installers.ProjectInstaller
{
    public class GameStateMachineInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            BindInterfaces();
            BindStateMachine();
        }

        public void Initialize()
        {
            Container.Resolve<GameStateMachine>().Enter<BootstrapState>();
        }

        private void BindInterfaces()
        {
            Container.BindInterfacesTo<GameStateMachineInstaller>().FromInstance(this);
        }
        
        private void BindStateMachine()
        {
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle().WithArguments(Container).NonLazy();
        }
    }
}