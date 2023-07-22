using System;
using UnityEngine;

namespace Code.Logic.Objects.Items
{
    [Serializable]
    public class ItemData
    {
        public ItemType Type;
        public float Value;
        public Sprite Sprite;
        public RuntimeAnimatorController AnimatorController;
    }
}