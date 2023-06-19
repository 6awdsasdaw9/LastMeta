using System.Linq;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Code.Debugers;
using Code.Infrastructure.GlobalEvents;
using Code.Logic.LanguageLocalization;
using Code.PresentationModel.HeadUpDisplay;
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
            if (flag)
            {
                if (_hud.GameMode == Constants.GameMode.Real)
                    return;

                _hud.HeroInformation.Button.OnStartTap += HeroInformationButtonOnStartTap;
            }
            else
            {
                if (_hud.GameMode == Constants.GameMode.Real)
                    return;
                _hud.HeroInformation.Button.OnStartTap -= HeroInformationButtonOnStartTap;
            }
        }

        private void SceneEventsOnOnInitHero()
        {
            var icon = _hud.HeroInformation.Window.HeroParamPanel.ParamIcons.FirstOrDefault(i =>
                i.upgradeParamType == HeroUpgradeParamType.Health);
            if (icon != null)
            {
                icon.SetDescription(_hero.Stats.MaxHeath.ToString());
            }

            icon = _hud.HeroInformation.Window.HeroParamPanel.ParamIcons.FirstOrDefault(i =>
                i.upgradeParamType == HeroUpgradeParamType.Attack);
            if (icon != null)
            {
                icon.SetDescription(_hero.Stats.Damage.ToString());
            }

            icon = _hud.HeroInformation.Window.HeroParamPanel.ParamIcons.FirstOrDefault(i =>
                i.upgradeParamType == HeroUpgradeParamType.Jump);
            if (icon != null)
            {
                icon.SetDescription(_hero.Stats.JumpHeight.ToString());
            }

            icon = _hud.HeroInformation.Window.HeroParamPanel.ParamIcons.FirstOrDefault(i =>
                i.upgradeParamType == HeroUpgradeParamType.Speed);
            if (icon != null)
            {
                icon.SetDescription(_hero.Stats.Speed.ToString());
            }
        }

        private void HeroInformationButtonOnStartTap()
        {
            if (_hud.HeroInformation.Window.IsOpen)
            {
                _hud.HeroInformation.Window.HideWindow(() => _hud.OnUIWindowHidden?.Invoke());
            }
            else
            {
                Logg.ColorLog("Show");
                SceneEventsOnOnInitHero();
                _hud.HeroInformation.Window.ShowWindow(() => _hud.OnUIWindowShown?.Invoke());
            }
        }
    }
}