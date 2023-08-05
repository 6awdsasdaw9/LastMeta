using System;
using Code.Data.Configs;
using Code.Services.SaveServices;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Logic.Collisions.Triggers
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
        private void Construct(SavedService service,SavedDataStorage savedDataStorage)
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