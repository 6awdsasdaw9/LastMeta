using System;
using Code.Infrastructure.GlobalEvents;
using Code.Logic.Objects;
using UnityEngine;
using Zenject;

namespace Code.Logic.Artifacts
{
    public class Item: MonoBehaviour
    {
        public DestroyedObjectAnimation Animator => _animator;
        [SerializeField] private DestroyedObjectAnimation _animator;

        [SerializeField] private SphereCollider _trigger;
        private ItemBehaviour _behaviour;
        public ItemData Data { get;private set; }

        public event Action<Item> OnPickUpItem;

        public void SetBehavior(ItemBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        private void OnTriggerEnter(Collider other)
        {
            _behaviour?.PickUp(() => OnPickUpItem?.Invoke(this));
            _trigger.enabled = false;
        }

        private void InitData(ItemData itemData)
        {
            Data = itemData;
            _animator.SetAnimatorController(Data.AnimatorController);
        }
        
        public class Pool : MonoMemoryPool<ItemData,EventsFacade ,Item>
        {
            protected override void OnCreated(Item item)
            {
                base.OnCreated(item);
            }

            protected override void Reinitialize(ItemData data, EventsFacade eventsFacade,Item item)
            {
                item.InitData(data);
                item.OnPickUpItem += _ => eventsFacade.ItemEvents.PickUpItemEvent(data); 
                base.Reinitialize(data,eventsFacade ,item);
            }

            protected override void OnDeSpawned(Item item)
            {
                item.OnPickUpItem = null;
                base.OnDeSpawned(item);
            }
        }
    }
}
