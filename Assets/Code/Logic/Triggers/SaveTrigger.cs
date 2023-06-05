using System;
using System.Drawing;
using Code.Data.Configs;
using Code.Data.ProgressData;
using Code.Debugers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Logger = Code.Debugers.Logger;

namespace Code.Logic.Triggers
{
    public class SaveTrigger : MonoBehaviour
    {
        public int ID;
        [SerializeField] private BoxCollider _collider;
        
        private SavedService _service;

        public PointData TriggerPointData => new()
        {
            ID = ID,
            Position = transform.position
        };

        [Inject]
        private void Construct(SavedService service,SavedDataCollection savedDataCollection)
        {
            _service = service;
        }

        private void Awake()
        {
            EnableCollider().Forget();
        }

        private async UniTaskVoid EnableCollider()
        {
            _collider.enabled = false;
            await UniTask.Delay(TimeSpan.FromSeconds(3),cancellationToken: gameObject.GetCancellationTokenOnDestroy());
            _collider.enabled = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            var currentScene = SceneManager.GetActiveScene().name;
            var pointData = new PointData
            {
                ID = ID,
                Position = transform.position
            };

            if (_service.SavedData.SceneSpawnPoints.ContainsKey(currentScene))
            {
                _service.SavedData.SceneSpawnPoints[currentScene] = pointData;
            }
            else
            {
                _service.SavedData.SceneSpawnPoints.Add(currentScene, pointData);
            }
            _service.SaveProgress();
            gameObject.SetActive(false);
        }
        
    }
}