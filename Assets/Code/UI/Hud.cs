using System;
using Code.UI.Actors;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.UI
{
    [RequireComponent(typeof(HudAdapter))]
    public class HUD : MonoBehaviour
    {
        [Title("Common HUD")]
        public InteractiveHudImage InteractiveImage;
        
        [Title("Game HUD")]
        public HpBar HeroHpBar;
        
        public Action OnUIWindowShown;
        public Action OnUIWindowHidden;
    }
}