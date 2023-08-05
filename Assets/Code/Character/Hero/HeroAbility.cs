using System;
using Code.Character.Hero.Abilities;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs.HeroConfigs;
using Code.Data.GameData;
using Code.Logic.Items;
using Code.Services.Input;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroAbility : MonoBehaviour, IHeroAbility
    {
        public HeroAbilityLevelData AbilityLevelData { get; private set; } = new();

        private IHero _hero;
        private HeroConfig _heroConfig;
        private InputService _inputService;
        private DiContainer _container;

        public HeroDashAbility DashAbility { get; private set; }
        public HeroHandAttackAbility HandAttackAbility { get; private set; }
        public HeroGunAttackAbility GunAttackAbility { get; private set; }
        public HeroSuperJumpAbility SuperJumpAbility { get; private set; }

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
            _inputService = container.Resolve<InputService>();
            _heroConfig = container.Resolve<HeroConfig>();
            _hero = GetComponent<IHero>();
        }

        public void Init(HeroAbilityLevelData abilityLevelData)
        {
            AbilityLevelData = abilityLevelData;
            SetHandAttackData(AbilityLevelData.HandLevel);
            SetDashData(AbilityLevelData.DashLevel);
            SetGunAttackData(AbilityLevelData.GunLevel);
            SetSuperJumpData(AbilityLevelData.SuperJumpLevel);
        }

        private void OnDestroy()
        {
            DashAbility?.StopApplying();
            DashAbility?.SubscribeToEvents(false);
        }

        #region Jump

        [Button]
        public void LevelUpSuperJump()
        {
            AbilityLevelData.SuperJumpLevel++;
            SetSuperJumpData(AbilityLevelData.SuperJumpLevel);
        }

        private void SetSuperJumpData(int level = 0)
        {
            SuperJumpAbility ??= new HeroSuperJumpAbility();
            level = CheckLevel<HeroSuperJumpAbility.Data>(level);
            SuperJumpAbility.SetData(_heroConfig.AbilitiesParams.SuperJumpData[level], level);
        }

        #endregion


        #region Gun

        [Button]
        public void LevelUpGunAttack()
        {
            AbilityLevelData.GunLevel++;
            SetGunAttackData(AbilityLevelData.HandLevel);
        }

        private void SetGunAttackData(int level)
        {
            GunAttackAbility ??= new HeroGunAttackAbility(_hero, _inputService);
            level = CheckLevel<ShootingParams>(level);
            GunAttackAbility.SetShootingParams(_heroConfig.AbilitiesParams.GunAttackLevelData[level], level);
        }

        #endregion

        #region Hand

        [Button]
        public void LevelUpHandAttack()
        {
            AbilityLevelData.HandLevel++;
            SetHandAttackData(AbilityLevelData.HandLevel);
        }

        private void SetHandAttackData(int level = 0)
        {
            HandAttackAbility ??= new HeroHandAttackAbility(_hero, _inputService);
            level = CheckLevel<HeroHandAttackAbility.Data>(level);
            HandAttackAbility.SetData(_heroConfig.AbilitiesParams.HandAttackLevelsData[level], level);
        }

        #endregion

        #region Dash

        [Button]
        public void LevelUpDash()
        {
            AbilityLevelData.DashLevel++;
            SetDashData(AbilityLevelData.DashLevel);
        }

        private void SetDashData(int level = 0)
        {
            DashAbility ??= new HeroDashAbility(_container);
            if (level == -1)
            {
                DashAbility.SetData(null, level);
                return;
            }

            level = CheckLevel<HeroDashAbility.Data>(level);
            DashAbility.SetData(_heroConfig.AbilitiesParams.DashLevelsData[level], level);
        }

        #endregion


        public Ability GetAbility(ItemType itemType)
        {
            return itemType switch
            {
                ItemType.RightSock => DashAbility,
                ItemType.LeftSock => SuperJumpAbility,
                ItemType.Glove => HandAttackAbility,
                ItemType.Gun => GunAttackAbility,
                ItemType.Substance => null,
                _ => null
            };
        }

        private int CheckLevel<T>(int level) where T : AbilitySettings
        {
            var maxLevel = _heroConfig.AbilitiesParams.GetMaxLevel<T>();
            if (level >= maxLevel) level = maxLevel;
            else if (level < 0) level = 0;
            return level;
        }
    }

    [Serializable]
    public class HeroAbilityLevelData
    {
        public int HandLevel;
        public int DashLevel;
        public int GunLevel;
        public int SuperJumpLevel;
    }
}