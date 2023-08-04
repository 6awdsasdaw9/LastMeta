using System;
using Cysharp.Threading.Tasks;

namespace Code.Logic.Items.ItemBehavious
{
    public abstract class ItemBehaviour
    {
        protected Item Item { get; }
        public ItemBehaviour(Item item)
        {
            Item = item;
        }

        public abstract void PickUp(Action OnPickUp = null);

        protected async UniTaskVoid InvokeActionWithDelay(Action Action, float delay)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            Action?.Invoke();
        }
    }
}