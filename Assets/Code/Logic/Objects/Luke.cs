using Code.Data.GameData;
using Code.Data.ProgressData;
using Code.Debugers;
using Code.Logic.Triggers;
using UnityEngine;

namespace Code.Logic.Objects
{
    public class Luke : MonoBehaviour, ISavedData
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private GameObject _groundCollider;
        [SerializeField] private UniqueId _id;

        private void Start()
        {
            Log.ColorLog(_id.Id, ColorType.Lightblue);
            _triggerObserver.TriggerEnter += TriggerEnter;
        }

        private void TriggerEnter(Collider obj)
        {
            _groundCollider.SetActive(false);
        }

        public void LoadData(SavedData savedData)
        {
            
        }

        public void SaveData(SavedData savedData)
        {
            
        }
    }
}