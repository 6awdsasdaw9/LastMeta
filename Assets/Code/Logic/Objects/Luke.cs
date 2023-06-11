using Code.Data;
using Code.Data.GameData;
using Code.Debugers;
using Code.Logic.Triggers;
using Code.Services;
using Code.Services.SaveServices;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Logic.Objects
{
    public class Luke : MonoBehaviour, ISavedData
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private Rigidbody _lidLukeObject;
        [SerializeField] private GameObject _groundCollider;
        [SerializeField] private UniqueId _uniqueId;

        [Inject]
        private void Construct(SavedDataStorage savedDataStorage)
        {
            savedDataStorage.Add(this);
        }

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
        }

        private void TriggerEnter(Collider obj)
        {
            _groundCollider.SetActive(false);
        }

 

        public void LoadData(SavedData savedData)
        {
            if (savedData.ObjectsPosition.ContainsKey(_uniqueId.Id))
            {
                _lidLukeObject.position = savedData.ObjectsPosition[_uniqueId.Id].AsUnityVector();
            }
        }

        public void SaveData(SavedData savedData)
        {
            if (!savedData.ObjectsPosition.ContainsKey(_uniqueId.Id))
            {
                savedData.ObjectsPosition.Add(_uniqueId.Id, _lidLukeObject.position.AsVectorData());
              
            }
            else
            {
                savedData.ObjectsPosition[_uniqueId.Id] = _lidLukeObject.position.AsVectorData();
            }
        }
    }
}