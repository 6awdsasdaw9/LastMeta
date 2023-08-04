using System;
using System.Linq;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs.HeroConfigs;
using Code.Infrastructure.GlobalEvents;
using Code.Services;
using Code.Services.EventsSubscribes;
using Code.UI.HeadUpDisplay.Windows.HudWindows.MenuWindowElements.Hero;
using UnityEngine;
using Zenject;

namespace Code.UI.HeadUpDisplay.Adapters
{
    public class HeroParamPanelAdapter: IEventsSubscriber
    {
        private readonly IHero _hero;
        private readonly HeroPanel _heroPanel;
        private readonly EventsFacade _eventsFacade;

        public HeroParamPanelAdapter(DiContainer container)
        {
            _hero = container.Resolve<IHero>();
            _heroPanel = container.Resolve<HudFacade>().Menu.Window.Hero;
            _eventsFacade = container.Resolve<EventsFacade>();
            
            container.Resolve<EventSubsribersStorage>().Add(this);
            
            SubscribeToEvents(true);
        }

        public void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _eventsFacade.SceneEvents.OnLoadScene += SetParamInIcons;
            }
            else
            {
                _eventsFacade.SceneEvents.OnLoadScene -= SetParamInIcons;
            }
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
            if (icon == null) return;
            icon.SetDescription(Math.Round(_hero.Stats.Speed).ToString());
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