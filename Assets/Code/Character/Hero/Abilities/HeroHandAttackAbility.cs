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
            _abilityCooldown = new Cooldown();
            
            _inputService = inputService;
            _inputService.OnPressSkillButtonOne += StartApplying;
        }

        public void SetData(Data data, int level)
        {
            Level = level;
            if(!IsOpen) return;
             CurrentData = data;
             Level = level;
            _abilityCooldown.SetMaxTime(data.attackData.Cooldown);
            _hero.HandAttack.SetDamageParam(data.attackData);
        }

        public override void StartApplying()
        {
            if(!IsOpen)return;
            _hero.ModeToggle.SetDefaultMode();
        }

        public override void StopApplying()
        {
        }

        
        [Serializable]
        public class Data : AbilitySettings
        {
            public AttackData attackData;
        }
    }
}