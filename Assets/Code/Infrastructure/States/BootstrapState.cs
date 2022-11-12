using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Facroty;
using Code.Infrastructure.Services;
using Code.Services.Input;

namespace Code.Infrastructure.States
{
    //Start work in GameBootstrapper.It is first gamestate
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadLevelState, string>("Main");


        private void RegisterServices()
        {
            Game.inputService = new InputService();
            AllServices.Container.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAssets>()));
        }

     
    }
}