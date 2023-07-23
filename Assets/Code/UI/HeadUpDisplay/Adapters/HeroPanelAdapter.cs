using System;
using System.Linq;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs.HeroConfigs;
using Code.Infrastructure.GlobalEvents;
using Code.UI.HeadUpDisplay.Windows.HudWindows.MenuWindowElements.Hero;
using UnityEngine;

namespace Code.UI.HeadUpDisplay.Adapters
{
    public class HeroPanelAdapter
    {
        private readonly EventsFacade _eventsFacade;
        private readonly HudFacade _hudFacade;
        private readonly IHero _hero;
        private readonly HeroPanel _heroPanel;

        public HeroPanelAdapter(EventsFacade eventsFacade, HudFacade hudFacade, IHero hero)
        {
            _eventsFacade = eventsFacade;
            _hudFacade = hudFacade;
            _hero = hero;
            _heroPanel = hudFacade.Menu.Window.Hero;
        }

        private void SetParamInIcons()
        {
            SetHealthParamInIcon();
            SetDamageParamInIcon();
            SetJumpHeightParamInIcon();
            SetSpeedParamInIcon();
        }

        private void SetSpeedParamInIcon()
        {
            var icon = _heroPanel.HeroParamPanel.ParamIcons.FirstOrDefault(i =>
                i.upgradeParamType == HeroUpgradeParamType.Speed);
            if (icon != null)
            {
                icon.SetDescription(Math.Round(_hero.Stats.Speed).ToString());
            }
        }

        private void SetJumpHeightParamInIcon()
        {
            var icon = _heroPanel.HeroParamPanel.ParamIcons.FirstOrDefault(i =>
                i.upgradeParamType == HeroUpgradeParamType.Jump);
            if (icon != null)
            {
                icon.SetDescription(Math.Round(_hero.Stats.JumpHeight).ToString());
            }
        }

        private void SetDamageParamInIcon()
        {
            var icon = _heroPanel.HeroParamPanel.ParamIcons.FirstOrDefault(i =>
                i.upgradeParamType == HeroUpgradeParamType.Damage);
            if (icon != null)
            {
                icon.SetDescription(Math.Round(_hero.Stats.Damage, 1).ToString());
            }
        }

        private void SetHealthParamInIcon()
        {
            var icon = _heroPanel.HeroParamPanel.ParamIcons.FirstOrDefault(i =>
                i.upgradeParamType == HeroUpgradeParamType.Health);
            if (icon != null)
            {
                icon.SetDescription(Mathf.Round(_hero.Stats.MaxHeath).ToString());
            }
        }
        
    }
}