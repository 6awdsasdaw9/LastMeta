using System;
using System.Collections.Generic;
using Code.UI.Adaptors;
using Code.UI.Windows;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.UI.HeadUpDisplay
{

    [RequireComponent(typeof(HudAdapter))]
    public class HUD : MonoBehaviour
    {
        public List<InteractiveObjectWindowData> InteractiveObjectWindows;

        [Title("Game HUD")]
        public HpBar HeroHpBar;

        public Action OnUIWindowShown;
        public Action OnUIWindowHidden;
    }
}