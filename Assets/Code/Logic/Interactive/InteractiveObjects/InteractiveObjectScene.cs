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
        [SerializeField] private Vector3 _exitPosition;
        
        private GameStateMachine _stateMachine;
        private PersistentSavedDataService _dataService;


        [Inject]
        private void Construct(GameStateMachine stateMachine,PersistentSavedDataService dataService)
        {
            _stateMachine = stateMachine;
            _dataService = dataService;
        }

        public override void StartInteractive()
        {
            _dataService.SaveProgress();
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