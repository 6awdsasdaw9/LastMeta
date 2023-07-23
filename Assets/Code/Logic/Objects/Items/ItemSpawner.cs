using Code.Data.AdditionalData;
using Code.Data.GameData;
using Code.Infrastructure.Factories;
using Code.Services.SaveServices;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Logic.Objects.Items
{
    public  class ItemSpawner : MonoBehaviour, ISavedData
    {
        [SerializeField] private ItemType _itemType;
        [SerializeField] private UniqueId _uniqueId;
        [SerializeField, Min(1)] private  int _itemsCount;

        [HideIf(nameof(_isSpawnOneItem)), SerializeField, MinMaxRange(-0.3f, 0.3f)]
        private RangedFloat _randomOffsetX;
        [HideIf(nameof(_isSpawnOneItem)), SerializeField, MinMaxRange(-0.3f, 0.3f)]
        private RangedFloat _randomOffsetY;
        private bool _isSpawnOneItem => _itemsCount == 1;
        private ItemFactory _factory;
        private bool _isPickUp;

        [Inject]
        private void Construct(ItemFactory itemFactory, SavedDataStorage savedDataStorage)
        {
            _factory = itemFactory;
            savedDataStorage.Add(this);
        }

        private void Spawn()
        {
            for (int i = 0; i < _itemsCount; i++)
            {
                var item = _factory.SpawnItem(_itemType);
                item.transform.position = transform.position + new Vector3(_randomOffsetX.GetRandom(), _randomOffsetY.GetRandom(), 0);
                item.OnPickUpItem += OnPickUpItem;
            }
        }

        private void OnPickUpItem(Item item)
        {
            _factory.DeSpawnItem(item);
            DisableSpawner();
        }

        private void DisableSpawner()
        {
            _isPickUp = true;
            enabled = false;
        }

        public void LoadData(SavedData savedData)
        {
            if (!savedData.Items.ContainsKey(_uniqueId.Id) || !savedData.Items[_uniqueId.Id])
            {
                Spawn();
                return;
            }
            DisableSpawner();
        }

        public void SaveData(SavedData savedData)
        {
            if (savedData.Items.ContainsKey(_uniqueId.Id))
            {
                savedData.Items[_uniqueId.Id] = _isPickUp;
            }
            else
            {
                savedData.Items.Add(_uniqueId.Id, _isPickUp);
            }
        }
    }
}