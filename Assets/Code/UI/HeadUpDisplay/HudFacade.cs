using System.Collections.Generic;
using Code.Logic.Objects.Animations;
using Code.UI.GameElements;
using Code.UI.HeadUpDisplay.HudElements;
using Code.UI.HeadUpDisplay.Windows.HudWindows.HudButtonWindows;
using Code.UI.HeadUpDisplay.Windows.InteractiveWindows;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.UI.HeadUpDisplay
{
    
    public class HudFacade : MonoBehaviour
    {
        [SerializeField] private Constants.GameMode _gameMode;
        public Constants.GameMode GameMode => _gameMode;
        public List<InteractiveObjectWindowData> InteractiveObjectWindows;
        
        public MenuButtonWindow Menu;
        private bool _isGameHud => _gameMode == Constants.GameMode.Game;

        [Space,Title("Game HUD")]
        [ShowIf(nameof(_isGameHud))] public HpBar HeroHpBar;
        [ShowIf(nameof(_isGameHud))] public TextPanel MoneyPanel;
   
        [Space]
        [ShowIf(nameof(_isGameHud))] public TextPanel DayPanel;
        [ShowIf(nameof(_isGameHud))] public HudSlider SliderTimeOfDay;
        [ShowIf(nameof(_isGameHud))] public StartStopAnimation HandleTimeOfDay;
   
    }
}