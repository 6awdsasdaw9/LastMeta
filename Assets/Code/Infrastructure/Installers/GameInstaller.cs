using Code.Infrastructure;
using Code.Infrastructure.States;
using Code.Logic;
using Code.Services;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>, IInitializable, ICoroutineRunner
{
    public bool isBindStateMachine = true;
    public LoadingCurtain curtain;
    
    
    public override void InstallBindings()
    {
        BindInterfaces();
        
        BindStateMachine();
        
        BindTimeOfDayController();
    }

    public void Initialize() => 
        Container.Resolve<GameStateMachine>().Enter<BootstrapState>();

    private void BindInterfaces() =>
        Container.BindInterfacesTo<GameInstaller>()
            .FromInstance(this);

    private void BindStateMachine()
    {
        if(!isBindStateMachine)
            return;
        
        Container.Bind<GameStateMachine>()
            .AsSingle().WithArguments(new SceneLoader(this), curtain).NonLazy();
    }

    private void BindTimeOfDayController()
    {
        Container.BindInterfacesAndSelfTo<TimeOfDayController>().AsSingle().NonLazy();
    }
}


