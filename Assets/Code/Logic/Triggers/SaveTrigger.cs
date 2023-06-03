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
        private SavedService _service;
        [SerializeField] private BoxCollider _collider;

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
        public PointData TriggerPointData => new PointData
        {
            ID = ID,
            Position = transform.position
        };

        [Inject]
        private void Construct(SavedService service,SavedDataCollection savedDataCollection)
        {
            _service = service;
        }

        private void OnTriggerEnter(Collider other)
        {
            Logger.ColorLog(other.name,ColorType.Orange);
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
        
        /*public void SaveData(SavedData savedData)
        {
            var currentScene = SceneManager.GetActiveScene().name;
            var pointData = new PointData
            {
                ID = ID,
                Position = transform.position
            };

            if (savedData.SceneSpawnPoints.ContainsKey(currentScene))
            {
                savedData.SceneSpawnPoints[currentScene] = pointData;
            }
            else
            {
                savedData.SceneSpawnPoints.Add(currentScene, pointData);
            }
            
            Log.ColorLog($"Progress Save In Trigger | {ID} | " +
                         $"{pointData.ID} |" +
                         $" {savedData.SceneSpawnPoints[currentScene].ID}", ColorType.Aqua);
        }*/
    }
}