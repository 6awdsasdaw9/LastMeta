using System;
using Code.Audio.AudioEvents;
using Code.Infrastructure.GlobalEvents;
using Code.Logic.Objects.Animations;
using Code.Logic.Objects.Items.ItemBehavious;
using UnityEngine;
using Zenject;

namespace Code.Logic.Objects.Items
{
    public class Item: MonoBehaviour
    {
        public DestroyedAnimation Animator => _animator;
        [SerializeField] private DestroyedAnimation _animator;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private SphereCollider _trigger;

        private AudioEvent _audioEvent;
        private ItemBehaviour _behaviour;
        public event Action<Item> OnPickUpItem;

        public ItemData Data { get;private set; }

        public void SetBehavior(ItemBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        private void OnTriggerEnter(Collider other)
        {
            _behaviour?.PickUp(() => OnPickUpItem?.Invoke(this));
            _audioEvent.PlayAudioEvent();
            _trigger.enabled = false;
        }

        private void InitData(ItemData itemData)
        {
            Data = itemData;
            _animator.SetAnimatorController(Data.AnimatorController);
            _audioEvent.SetEventReference(itemData.AudioEvent);
            _spriteRenderer.sprite = Data.Sprite;
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
