using System;
using System.Threading;
using Code.Data.Configs.HeroConfigs;
using Code.Services;

namespace Code.Character.Hero.Abilities
{
    public class HeroSuperJumpAbility: Ability
    {

        public Data CurrentData;
        
        private CancellationTokenSource _abilityCts;
        private readonly Cooldown _abilityCooldown;
        
        public HeroSuperJumpAbility()
        {
            Type = HeroAbilityType.Hand;
        }

        public void SetData(Data data)
        {
            CurrentData = data;
        }
        public override void OpenAbility()
        {
            if (IsOpen)
                return;

            IsOpen = true;
          
        }

        public override void StartApplying()
        {
        
        }

        public override void StopApplying()
        {
        }

        
        [Serializable]
        public class Data : AbilitySettings
        {
            public int MaxAirJump;
            public int BonusHeightJump;
        }
    }
}