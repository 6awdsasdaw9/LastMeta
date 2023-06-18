using System;
using System.Threading;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Code.Data.GameData;
using Code.Debugers;
using Code.Services;
using Code.Services.Input;
using UnityEngine;

namespace Code.Character.Hero
{
    public class HeroHandAttackAbility : Ability
    {
        private readonly IHero _hero;
        private readonly InputService _inputService;
        private readonly HeroConfig _heroConfig;
        private Data _currentData;
        
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
            _currentData = data;
            _abilityCooldown.SetTime(data.Cooldown);
            _hero.HandAttack.SetDamageParam(data.DamageParam);
        }
        public override void OpenAbility()
        {
            if (IsOpen)
                return;

            IsOpen = true;
            _inputService.OnPressSkillButtonOne += StartApplying;
        }

        public override void StartApplying()
        {
            Logg.ColorLog("HeroHandAttackAbility: StartApplying");
            _hero.HeroMode.SetDefaultMode();
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