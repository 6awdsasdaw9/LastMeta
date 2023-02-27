using Code.Infrastructure.States;
using Code.Logic;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
    //Enter Point
    public class BootstrapInstaller : MonoBehaviour, ICoroutineRunner
    {
        public LoadingCurtain curtain;
        private Game _game;

 
        private void Awake()
        {
            _game = new Game(this,curtain);
            _game.stateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}