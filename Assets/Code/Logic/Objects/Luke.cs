using Code.Data.GameData;
using Code.Data.ProgressData;
using Code.Debugers;
using Code.Logic.Triggers;
using Code.Services;
using UnityEngine;
using Zenject;

namespace Code.Logic.Objects
{
    public class Luke : MonoBehaviour, ISavedData
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private Transform _lidLukeObject;
        [SerializeField] private GameObject _groundCollider;
        [SerializeField] private UniqueId _uniqueId;

        [Inject]
        private void Construct(SavedDataCollection savedDataCollection)
        {
            savedDataCollection.Add(this);
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
            Log.ColorLog($"LOAD: luke position  {savedData.ObjectsPosition.ContainsKey(_uniqueId.Id)}",
                ColorType.Purple);
            if (savedData.ObjectsPosition.ContainsKey(_uniqueId.Id))
            {
                _lidLukeObject.position = savedData.ObjectsPosition[_uniqueId.Id].AsUnityVector();
                Log.ColorLog($"LOAD: luke position = {_lidLukeObject.position}", ColorType.Purple);
            }
        }

        public void SaveData(SavedData savedData)
        {
            if (!savedData.ObjectsPosition.ContainsKey(_uniqueId.Id))
            {
                savedData.ObjectsPosition.Add(_uniqueId.Id, _lidLukeObject.position.AsVectorData());
                Log.ColorLog($"IF || SAVED DATA: luke position = {savedData.ObjectsPosition[_uniqueId.Id].AsUnityVector()}", ColorType.Orange);
            }
            else
            {
                savedData.ObjectsPosition[_uniqueId.Id] = _lidLukeObject.position.AsVectorData();
                Log.ColorLog($"ELSE || SAVED DATA: luke position = {savedData.ObjectsPosition[_uniqueId.Id].AsUnityVector()}", ColorType.Orange);
            }

            Log.ColorLog($"SAVE: luke position = {_lidLukeObject.position}", ColorType.Magenta);
        }
    }
}