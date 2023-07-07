using System;
using System.Linq;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Code.Debugers;
using Code.Infrastructure.GlobalEvents;
using Code.Logic.LanguageLocalization;
using Code.PresentationModel.HeadUpDisplay;
using UnityEngine;
using Zenject;

namespace Code.Logic.Adaptors
{
    public class HeroInformationWindowAdapter
    {
        private readonly EventsFacade _eventsFacade;
        private readonly Hud _hud;
        private readonly IHero _hero;

        public HeroInformationWindowAdapter(EventsFacade eventsFacade, Hud hud, IHero hero)
        {
            _eventsFacade = eventsFacade;
            _hud = hud;
            _hero = hero;
        }

        public void SubscribeToEvent(bool flag)
        {
            if (_hud.GameMode == Constants.GameMode.Real)
                return;
            if (flag)
            {
                _hud.HeroInformation.Button.OnStartTap += HeroInformationButtonOnStartTap;
            }
            else
            {
                _hud.HeroInformation.Button.OnStartTap -= HeroInformationButtonOnStartTap;
            }
        }

        private void SetParamInIcons()
        {
            var icon = _hud.HeroInformation.Window.HeroParamPanel.ParamIcons.FirstOrDefault(i =>
                i.upgradeParamType == HeroUpgradeParamType.Health);
            if (icon != null)
            {
                icon.SetDescription(Mathf.Round(_hero.Stats.MaxHeath).ToString());
            }

            icon = _hud.HeroInformation.Window.HeroParamPanel.ParamIcons.FirstOrDefault(i =>
                i.upgradeParamType == HeroUpgradeParamType.Attack);
            if (icon != null)
            {
                icon.SetDescription(Math.Round(_hero.Stats.Damage,1).ToString());
            }

            icon = _hud.HeroInformation.Window.HeroParamPanel.ParamIcons.FirstOrDefault(i =>
                i.upgradeParamType == HeroUpgradeParamType.Jump);
            if (icon != null)
            {
                icon.SetDescription(Math.Round(_hero.Stats.JumpHeight).ToString());
            }

            icon = _hud.HeroInformation.Window.HeroParamPanel.ParamIcons.FirstOrDefault(i =>
                i.upgradeParamType == HeroUpgradeParamType.Speed);
            if (icon != null)
            {
                icon.SetDescription(Math.Round(_hero.Stats.Speed).ToString());
            }
        }

        private void HeroInformationButtonOnStartTap()
        {
            if (_hud.HeroInformation.Window.IsOpen)
            {
                _hud.HeroInformation.Window.HideWindow(() =>
                    _eventsFacade.HudEvents.WindowHiddenEvent(_hud.HeroInformation.Window));
            }
            else
            {
                SetParamInIcons();
                _hud.HeroInformation.Window.ShowWindow(() => 
                    _eventsFacade.HudEvents.WindowShownEvent(_hud.HeroInformation.Window));
            }
        }
    }
}