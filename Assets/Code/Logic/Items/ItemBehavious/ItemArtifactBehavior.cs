using System;

namespace Code.Logic.Objects.Items.ItemBehavious
{
    [Serializable]
    public class ItemArtifactBehavior : ItemBehaviour
    {
        
        public ItemArtifactBehavior(Item item) : base(item)
        {
        }

        public override void PickUp(Action OnPickUp = null)
        {
            Item.Animator.PlayDestroy();
            InvokeActionWithDelay(() => OnPickUp?.Invoke(), 1f).Forget();
        }
    }

    public class ItemMoneyBehavior : ItemBehaviour
    {
        public ItemMoneyBehavior(Item item) : base(item)
        {
        }

        public override void PickUp(Action OnPickUp = null)
        {
            Item.Animator.PlayDestroy();
            InvokeActionWithDelay(() => OnPickUp?.Invoke(), 1f).Forget();
        }
    }
    
    
}