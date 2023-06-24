using System;
using Code.Character.Hero;
using Sirenix.OdinInspector;

namespace Code.Logic.Artifacts.Artifacts
{
    [Serializable]
    public abstract class ItemArtifactBehavior : ItemBehaviour
    {
        public ItemArtifactBehavior(Item item) : base(item)
        {
        }

        public override void PickUp()
        {
            
        }
    }
}