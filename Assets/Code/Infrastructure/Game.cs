using Code.Infrastructure.States;
using Code.Logic;
using Code.Services;
using Code.Services.Input;

namespace Code.Infrastructure
{
    //Create in the GameBootstrapper before bootstrappState
    public class Game
    {
        //public static InputService inputService;
        public readonly GameStateMachine stateMachine;
        
        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            stateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain);
        }
    }
}