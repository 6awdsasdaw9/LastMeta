using System;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Code.Debugers;
using Code.Services.Input;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroAbility : MonoBehaviour, IHeroAbility
    {
        public HeroAbilityLevelData AbilityLevelData { get; private set; }

        private IHero _hero;
        private HeroConfig _heroConfig;
        private InputService _inputService;

        public HeroDashAbility DashAbility { get; private set; }
        public HeroHandAttackAbility HandAttackAbility { get; private set; }
        public HeroGunAttackAbility GunAttackAbility { get; private set; }

        public HeroSuperJumpAbility SuperJumpAbility { get; private set; }

        [Inject]
        private void Construct(InputService inputService, HeroConfig heroConfig)
        {
            _inputService = inputService;
            _heroConfig = heroConfig;
            _hero = GetComponent<IHero>();
        }
        
        public void Init(HeroAbilityLevelData abilityLevelData)
        {
            Logg.ColorLog($"init is null {abilityLevelData == null}");

            AbilityLevelData = abilityLevelData;
            OpenHandAttack(AbilityLevelData.HandLevel);
            /*OpenDash(AbilityLevelData.DashLevel);
            OpenGunAttack(AbilityLevelData.GunLevel);
            OpenSuperJump();*/
        }

        public void LevelUpSuperJump()
        {
            if (SuperJumpAbility is not { IsOpen: true })
            {
                OpenGunAttack(AbilityLevelData.SuperJumpLevel);
                return;
            }

            AbilityLevelData.SuperJumpLevel++;
            SuperJumpAbility?.SetData(_heroConfig.AbilitiesParams.SuperJumpData[AbilityLevelData.SuperJumpLevel]);
        }

        private void OpenSuperJump(int level = 0)
        {
            SuperJumpAbility = new HeroSuperJumpAbility();
            level = CheckLevel<HeroSuperJumpAbility.Data>(level);
            SuperJumpAbility.SetData(_heroConfig.AbilitiesParams.SuperJumpData[level]);
            SuperJumpAbility.OpenAbility();
        }
        [Button]
        public void LevelUpGunAttack()
        {
            if (GunAttackAbility is not { IsOpen: true })
            {
                OpenGunAttack(AbilityLevelData.HandLevel);
                return;
            }

            AbilityLevelData.GunLevel++;
            HandAttackAbility?.SetData(_heroConfig.AbilitiesParams.HandAttackLevelsData[AbilityLevelData.HandLevel]);
        }
        private void OpenGunAttack(int level)
        {
            GunAttackAbility = new HeroGunAttackAbility(_hero, _inputService);
            level = CheckLevel<ShootingParams>(level);
            GunAttackAbility.SetShootingParams(_heroConfig.AbilitiesParams.GunAttackLevelData[level]);
            GunAttackAbility.OpenAbility();
        }


        [Button]
        public void LevelUpHandAttack()
        {
            if (HandAttackAbility is not { IsOpen: true })
            {
                OpenHandAttack(AbilityLevelData.HandLevel);
                return;
            }

            AbilityLevelData.HandLevel++;
            HandAttackAbility?.SetData(_heroConfig.AbilitiesParams.HandAttackLevelsData[AbilityLevelData.HandLevel]);
        }
        private void OpenHandAttack(int level = 0)
        {
            HandAttackAbility = new HeroHandAttackAbility(_hero, _inputService);
            level = CheckLevel<HeroHandAttackAbility.Data>(level);
            HandAttackAbility.SetData(_heroConfig.AbilitiesParams.HandAttackLevelsData[level]);
            HandAttackAbility.OpenAbility();
        }


        [Button]
        public void LevelUpDash()
        {
            if (AbilityLevelData == null)
            {
                Logg.ColorLog("=((((((");
                return;
            }
            if (DashAbility is not { IsOpen: true } )
            {
                OpenDash(AbilityLevelData.DashLevel);
                return;
            }
            AbilityLevelData.DashLevel++;
            DashAbility.SetData(_heroConfig.AbilitiesParams.DashLevelsData[AbilityLevelData.DashLevel]);
        }

        private void OpenDash(int level = 0)
        {
            DashAbility = new HeroDashAbility(_hero, _inputService);
            level = CheckLevel<HeroDashAbility.Data>(level);
            DashAbility.SetData(_heroConfig.AbilitiesParams.DashLevelsData[level]);
            DashAbility.OpenAbility();
        }

        private void OnDisable()
        {
            DashAbility?.StopApplying();
        }

        private int CheckLevel<T>(int level) where T : AbilitySettings
        {
            var maxLevel = _heroConfig.AbilitiesParams.GetMaxLevel<T>();
            if (level >= maxLevel)
            {
                level = maxLevel;
            }
            else if (level < 0)
                level = 0;

            return level;
        }
    }

    [Serializable]
    public class HeroAbilityLevelData
    {
        public int DashLevel;
        public int GunLevel;
        public int HandLevel;
        public int SuperJumpLevel;
    }
}