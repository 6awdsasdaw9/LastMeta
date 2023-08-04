using Code.Infrastructure.StateMachine;
using Code.Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Code.Logic.Triggers
{
    public class LevelTransferTrigger : MonoBehaviour
    {
        [SerializeField] private Constants.Scenes _nextScene;
        private GameStateMachine _stateMachine;
        private bool _triggered;

        [Inject]
        public void Construct(GameStateMachine stateMachine) =>
            _stateMachine = stateMachine;

        private void OnTriggerEnter(Collider other)
        {
            if (_triggered || !other.CompareTag(Constants.PlayerTag))
                return;
            
            _stateMachine.Enter<LoadLevelState, string>(_nextScene.ToString());
            _triggered = true;
        }
    }
}