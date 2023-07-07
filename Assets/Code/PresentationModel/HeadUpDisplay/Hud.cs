using System;
using System.Collections.Generic;
using Code.PresentationModel.Buttons;
using Code.PresentationModel.HudElements.HudButtonWindows;
using Code.PresentationModel.Windows.InteractiveWindows;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.PresentationModel.HeadUpDisplay
{
    
    public class Hud : MonoBehaviour
    {
        [SerializeField] private Constants.GameMode _gameMode;
        public Constants.GameMode GameMode => _gameMode;
        public List<InteractiveObjectWindowData> InteractiveObjectWindows;
        
        public MenuButtonWindow Menu;
        private bool _isGameHud => _gameMode == Constants.GameMode.Game;
        [Space,Title("Game HUD")]
        [ShowIf(nameof(_isGameHud))] public HpBar HeroHpBar;
        [ShowIf(nameof(_isGameHud))] public TextPanel DayPanel;
        [ShowIf(nameof(_isGameHud))] public HudSlider SliderTimeOfDay;
        [ShowIf(nameof(_isGameHud))] public TextPanel MoneyPanel;
        [ShowIf(nameof(_isGameHud))] public HeroInformationButtonWindow HeroInformation;
        
    
    }
}