using System;
using System.Collections.Generic;
using Code.Logic.Adaptors;
using Code.PresentationModel.Buttons;
using Code.PresentationModel.Windows.InteractiveWindows;
using Code.PresentationModel.Windows.MenuWindow;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.PresentationModel.HeadUpDisplay
{
    
    public class Hud : MonoBehaviour
    {
        [SerializeField] private Constants.GameMode _gameMode;
        public List<InteractiveObjectWindowData> InteractiveObjectWindows;
        
        public MenuWindow MenuWindow;
        public HudButton MenuHudButton;
        
        private bool _isGameHud => _gameMode == Constants.GameMode.Game;
        [Space,Title("Game HUD")]
        [ShowIf(nameof(_isGameHud))] public HpBar HeroHpBar;
        [ShowIf(nameof(_isGameHud))] public TextPanel DayPanel;
        [ShowIf(nameof(_isGameHud))] public HudSlider SliderTimeOfDay;
        [ShowIf(nameof(_isGameHud))] public HudButton HeroMenuHudButton;
        [ShowIf(nameof(_isGameHud))] public TextPanel MoneyPanel;
        
        public Action OnUIWindowShown;
        public Action OnUIWindowHidden;
    }
}