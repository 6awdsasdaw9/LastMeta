using System.Linq;
using Code.Data.Configs;
using Code.Data.ProgressData;
using Code.Debugers;
using Code.Infrastructure.StateMachine;
using Code.Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;
using Logger = Code.Debugers.Logger;

namespace Code.Logic.Interactive.InteractiveObjects
{
    public class InteractiveObjectScene : Interactivity
    {
        [SerializeField] private Constants.Scenes _interactiveScene;
        [SerializeField] private int _spawnPointID;

        private GameStateMachine _stateMachine;
        private SavedService _savedService;
        private SpawnPointsConfig _spawnPointsConfig;
        
        [Inject]
        private void Construct(GameStateMachine stateMachine, SavedService savedService,
            SpawnPointsConfig spawnPointsConfig)
        {
            _stateMachine = stateMachine;
            _savedService = savedService;
            _spawnPointsConfig = spawnPointsConfig;
        }

        public override void StartInteractive()
        {
            SaveNewSpawnPoint();
            _stateMachine.Enter<LoadLevelState, string>(_interactiveScene.ToString());
            StopInteractive();
        }

        private void SaveNewSpawnPoint()
        {
            var points = _spawnPointsConfig.SceneSpawnPoints.FirstOrDefault(p => p.Scene == _interactiveScene)?.Points;
            if (points == null)
            {
                Logger.ColorLog("Interactive object scene: Can'not find points in Spawn Points Config", LogStyle.Error);
                return;
            }

            var spawnPoint = points.FirstOrDefault(p => p.ID == _spawnPointID);
            if (spawnPoint == null)
            {
                Logger.ColorLog("Interactive object scene: Can'not find a spawn point by ID", LogStyle.Error);
                return;
            }

            if (_savedService.SavedData.SceneSpawnPoints.ContainsKey(_interactiveScene.ToString()))
            {
                _savedService.SavedData.SceneSpawnPoints[_interactiveScene.ToString()] = spawnPoint;
            }
            else
            {
                _savedService.SavedData.SceneSpawnPoints.Add(_interactiveScene.ToString(), spawnPoint);
            }

            _savedService.SaveProgress();
        }

        public override void StopInteractive()
        {
        }
    }
}