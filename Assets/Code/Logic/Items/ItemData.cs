using System;
using FMODUnity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Logic.Items
{
    [Serializable]
    public class ItemData
    {
        public ItemType Type;
        public float Value;
        public Sprite Sprite;
        public Sprite Icon;
        public RuntimeAnimatorController AnimatorController;
        [GUIColor(0.85f, 0.74f, 1)]
        public EventReference AudioEvent;
    }
}