using System;
using Code.Character.Hero;
using Sirenix.OdinInspector;

namespace Code.Logic.Artifacts.Artifacts
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