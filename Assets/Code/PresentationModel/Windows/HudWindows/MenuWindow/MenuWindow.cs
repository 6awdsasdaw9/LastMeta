using System;
using Code.PresentationModel.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace Code.PresentationModel.Windows.MenuWindow
{
    public class MenuWindow : HudElement, IHudWindow
    {
        [SerializeField] private HudSlider _musicVolumeHudSlider;
        public HudSlider MusicVolumeHudSlider => _musicVolumeHudSlider;
        
        [SerializeField] private HudSlider _effectVolumeHudSlider;
        public HudSlider EffectVolumeHudSlider => _effectVolumeHudSlider;
        
        [SerializeField] private Toggle _muteToggle;
        public Toggle MuteToggle => _muteToggle;
        
        [SerializeField] private HudButton _closeWindowButton;
        public HudButton CloseButton => _closeWindowButton;
        
        [SerializeField] private HudButton _hudButton;
        public HudButton HudButton => _hudButton;
        public void ShowWindow(Action WindowShowed = null)
        {
            
        }

        public void HideWindow(Action WindowHidden = null)
        {
            
        }

    }
}