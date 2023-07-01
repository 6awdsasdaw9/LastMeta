using Code.Character.Enemies;
using Code.Data.GameData;
using Code.Infrastructure.Factories;
using Code.Services.SaveServices;
using UnityEngine;
using Zenject;

namespace Code.Logic.Artifacts
{
    public  class ItemSpawner : MonoBehaviour, ISavedData
    {
        [SerializeField] private ItemType _itemType;
        [SerializeField] private  int _itemsCount;
        [SerializeField] private UniqueId _uniqueId;
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
            var item = _factory.SpawnItem(_itemType);
            item.transform.position = transform.position;
            item.OnPickUpItem += OnPickUpItem;
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