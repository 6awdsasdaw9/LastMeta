using System;
using System.Collections.Generic;
using Code.UI.Adaptors;
using Code.UI.Windows;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.UI
{
    public interface IHUD
    {
        public Action OnUIWindowShown { get; }
        public Action OnUIWindowHidden{ get; }
    }

    [RequireComponent(typeof(HudAdapter))]
    public class HUD : MonoBehaviour
    {
        [Title("Common HUD")]
        public InteractiveImageWindow InteractiveImageWindow;

        public List<InteractiveObjectWindowData> InteractiveObjectWindows;

        [Title("Game HUD")]
        public HpBar HeroHpBar;

        public Action OnUIWindowShown;
        public Action OnUIWindowHidden;
    }
}