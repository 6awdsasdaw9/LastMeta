using System;
using UnityEditor.Animations;
using UnityEngine;

namespace Code.Logic.Artifacts
{
    [Serializable]
    public class ItemData
    {
        public ItemType Type;
        public float Value;
        public RuntimeAnimatorController AnimatorController;
    }
}