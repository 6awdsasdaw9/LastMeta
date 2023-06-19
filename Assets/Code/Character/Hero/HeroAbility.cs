using System;
using System.Collections;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
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

        public HeroDashAbility HeroDashAbility { get; private set; }
        public HeroHandAttackAbility HandAttackAbility{ get; private set; }
        public HeroGunAttackAbility GunAttackAbility{ get; private set; }

        [Inject]
        private void Construct(InputService inputService, HeroConfig heroConfig)
        {
            _inputService = inputService;
            _heroConfig = heroConfig;
            _hero = GetComponent<IHero>();
        }


        public void Init(HeroAbilityLevelData abilityLevelData)
        {
            AbilityLevelData = abilityLevelData;
            OpenDash(AbilityLevelData.DashLevel);
            OpenHandAttack(AbilityLevelData.HandLevel);
            OpenGunAttack(AbilityLevelData.GunLevel);
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
            AbilityLevelData.HandLevel++;
            HandAttackAbility.SetData(_heroConfig.AbilitiesParams.HandAttackLevelsData[ AbilityLevelData.HandLevel]);
        }

        public void OpenHandAttack(int level = 0)
        {
            HandAttackAbility = new HeroHandAttackAbility(_hero, _inputService);
            HandAttackAbility.SetData(_heroConfig.AbilitiesParams.HandAttackLevelsData[level]);
            HandAttackAbility.OpenAbility();
        }

        [Button]
        public void LevelUpDash()
        {
            if (HeroDashAbility == null)
            {
                OpenDash();
            }
            else
            {
                AbilityLevelData.DashLevel++;
                HeroDashAbility.SetData(_heroConfig.AbilitiesParams.DashLevelsData[AbilityLevelData.DashLevel]);
            }
        }

        public void OpenDash(int level = 0)
        {
            HeroDashAbility = new HeroDashAbility(_hero, _inputService);
            HeroDashAbility.SetData(_heroConfig.AbilitiesParams.DashLevelsData[level]);
            HeroDashAbility.OpenAbility();
        }

        private void OnDisable()
        {
            HeroDashAbility?.StopApplying();
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
    }
}