using System;
using System.Threading;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Code.Data.GameData;
using Code.Services;
using Code.Services.Input;

namespace Code.Character.Hero
{
    public class HeroGunAttackAbility : Ability
    {
        private readonly IHero _hero;
        private readonly InputService _inputService;
        private readonly HeroConfig _heroConfig;
        private Data _currentData;
        
        private CancellationTokenSource _abilityCts;
        
        private readonly Cooldown _abilityCooldown;
        
        
        public HeroGunAttackAbility(IHero hero, InputService inputService)
        {
            Type = HeroAbilityType.Gun;

            _hero = hero;
            _inputService = inputService;

            _abilityCooldown = new Cooldown();
        }

        public void SetData(Data data)
        {
            _currentData = data;
            _abilityCooldown.SetTime(data.Cooldown);
            _hero.HandAttack.SetDamageParam(data.DamageParam);
        }
        public override void OpenAbility()
        {
            if (IsOpen)
                return;

            IsOpen = true;
            _inputService.OnPressSkillButtonTwo += StartApplying;
        }

        public override void StartApplying()
        {
            
            _hero.HeroMode.SetGunMode();
        }

        public override void StopApplying()
        {
        }

        
        [Serializable]
        public class Data
        {
            public float Cooldown;
            public DamageParam DamageParam;
        }
    }
}