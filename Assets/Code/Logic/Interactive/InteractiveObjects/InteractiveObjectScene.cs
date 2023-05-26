using Code.Data.ProgressData;
using Code.Debugers;
using Code.Infrastructure.StateMachine;
using Code.Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Code.Logic.Interactive.InteractiveObjects
{
    public class InteractiveObjectScene : Interactivity
    {
        [SerializeField] private Constants.Scenes _interactiveScene;
        [SerializeField] private int _spawnPointID;
        
        private GameStateMachine _stateMachine;
        private SavedService _service;


        [Inject]
        private void Construct(GameStateMachine stateMachine,SavedService service)
        {
            _stateMachine = stateMachine;
            _service = service;
        }

        public override void StartInteractive()
        {
            _service.SaveProgress();
            Log.ColorLog($"Enter to {_interactiveScene.ToString()} || Progress Save ",ColorType.Orange);
            _stateMachine.Enter<LoadLevelState, string>(_interactiveScene.ToString());
            StopInteractive();
        }

        public override void StopInteractive()
        {
            Log.ColorLog($"Stop Scene Interactive  ",ColorType.Orange);
        }
    }
}