using System;
using System.Threading;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs.HeroConfigs;
using Code.Data.GameData;
using Code.Debugers;
using Code.Services;
using Code.Services.Input;

namespace Code.Character.Hero.Abilities
{
    public class HeroHandAttackAbility : Ability
    {
        private readonly IHero _hero;
        private readonly InputService _inputService;
        private readonly HeroConfig _heroConfig;
        public Data CurrentData;
        
        private CancellationTokenSource _abilityCts;
        
        private readonly Cooldown _abilityCooldown;
        
        
        public HeroHandAttackAbility(IHero hero, InputService inputService)
        {
            Type = HeroAbilityType.Hand;

            _hero = hero;
            _inputService = inputService;

            _abilityCooldown = new Cooldown();
        }

        public void SetData(Data data)
        {
             CurrentData = data;
            _abilityCooldown.SetTime(data.Cooldown);
            _hero.HandAttack.SetDamageParam(data.DamageParam);
        }
        public void OpenAbility()
        {
            if (IsOpen)
                return;

            IsOpen = true;
            _inputService.OnPressSkillButtonOne += StartApplying;
        }

        public override void StartApplying()
        {
            Logg.ColorLog("HeroHandAttackAbility: StartApplying");
            _hero.ModeToggle.SetDefaultMode();
        }

        public override void StopApplying()
        {
        }

        
        [Serializable]
        public class Data : AbilitySettings
        {
            public float Cooldown;
            public DamageParam DamageParam;
        }
    }
}