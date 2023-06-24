using Code.Infrastructure.GlobalEvents;
using UnityEngine;
using Zenject;

namespace Code.Logic.Artifacts
{
    public class Item: MonoBehaviour
    {
        private ItemBehaviour _behaviour;
        
        [SerializeField] private Animator _animator;
        public Animator Animator => _animator;
        
        public ItemData Data { get; private set; }

        public void SetBehavior(ItemBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        private void InitData(ItemData itemData)
        {
            Data = itemData;
            _animator.runtimeAnimatorController = Data.AnimatorController;
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
                base.Reinitialize(data,eventsFacade ,item);
            }

            protected override void OnDeSpawned(Item item)
            {
                base.OnDeSpawned(item);
            }
        }
    }
}
