using System;
using System.Collections.Generic;
using Code.UI.Adaptors;
using Code.UI.Windows;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.UI.HeadUpDisplay
{

    [RequireComponent(typeof(HudAdapter))]
    public class Hud : MonoBehaviour
    {
        [SerializeField] private Constants.GameMode _gameMode;
        public List<InteractiveObjectWindowData> InteractiveObjectWindows;

        private bool _isGameHud => _gameMode == Constants.GameMode.Game;
        [Space,Title("Game HUD")]
        [ShowIf(nameof(_isGameHud))] public HpBar HeroHpBar;
        [ShowIf(nameof(_isGameHud))] public SliderController SliderTimeOfDay;

        public Action OnUIWindowShown;
        public Action OnUIWindowHidden;
    }
}