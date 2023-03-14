using Code.Infrastructure.StateMachine;
using Code.Infrastructure.StateMachine.States;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Logic
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
            if(_triggered)
                return;

            if (other.CompareTag(Constants.PlayerTag))
            {
                _stateMachine.Enter<LoadLevelState, string>(_nextScene.ToString());
                _triggered = true;
            }
        }
    }
}