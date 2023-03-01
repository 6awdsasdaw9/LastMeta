using Code.Infrastructure;
using Code.Infrastructure.States;
using Code.Logic;
using Zenject;

public class BootstrapInstaller : MonoInstaller<BootstrapInstaller>, IInitializable
{
    public LoadingCurtain curtain;


    //  private Game _game;
    public override void InstallBindings()
    {
        BindInterfaces();
        BindStateMachine();
        BindTest();
    }

    private void BindInterfaces()
    {
        Container.BindInterfacesTo<BootstrapInstaller>()
            .FromInstance(this);
    }

    private void BindStateMachine()
    {
        /*Container.BindInterfacesTo<GameStateMachine>()
            .AsSingle().NonLazy();*/
    }

    private void BindTest()
    {
        Container.BindInterfacesTo<MyTest>()
            .AsSingle().NonLazy();
    }
    
    public void Initialize()
    {
        
    }
}