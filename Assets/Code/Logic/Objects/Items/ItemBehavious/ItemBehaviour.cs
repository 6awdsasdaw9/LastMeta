namespace Code.Logic.Artifacts
{
    public abstract class ItemBehaviour
    {
        public  ItemBehaviour(Item item)
        {
            Item = item;
        }
        public Item Item { get; private set; }
        public abstract void PickUp();
    }
}