using System;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Code.Data.Configs.HeroConfigs;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using HeroConfig = Code.Data.Configs.HeroConfigs.HeroConfig;

namespace Code.Character.Hero
{
    public class HeroUpgrade : MonoBehaviour,  IHeroUpgrade
    {
        public HeroUpgradesLevelData UpgradesLevel { get; private set; } = new();
        public float BonusSpeed { get; private set; }
        public float BonusHeightJump { get; private set; }
        public int BonusAttack { get; private set; }
        public float BonusHealth { get;private set; }

        private HeroUpgradesParams _upgradesParams;
        
        [Inject]
        private void Construct(HeroConfig heroConfig)
        {
            _upgradesParams = heroConfig.UpgradesParams;
        }

        public void Init(HeroUpgradesLevelData heroUpgradesLevelData)
        {
            UpgradesLevel = heroUpgradesLevelData;
            SetSpeed();
            SetHeightJump();
            SetAttack();
            SetBonusHealth();
        }

        [Button]
        private void LevelUpHealth()
        {
            if(UpgradesLevel.HealthLevel >= _upgradesParams.GetMaxLevel<HeroUpgradeData>())
                return;
            
            UpgradesLevel.HealthLevel++;
            SetBonusHealth();
        }

        [Button]
        public void LevelUpSpeed()
        {
            if(UpgradesLevel.SpeedLevel >=_upgradesParams.GetMaxLevel<SpeedUpgradesData>())
                return;
            
            UpgradesLevel.SpeedLevel++;
            SetSpeed();
        }
        
        [Button]
        public void LevelUpJump()
        {
            if(UpgradesLevel.JumpHeightLevel >= _upgradesParams.GetMaxLevel<JumpUpgradesData>())
                return;
            UpgradesLevel.JumpHeightLevel++;
            SetHeightJump();
        }

        [Button]
        public void LevelUpAttack()
        {
            if(UpgradesLevel.AttackLevel >= _upgradesParams.GetMaxLevel<AttackUpgradesData>())
                return;
            UpgradesLevel.AttackLevel++;
            SetAttack();
        }

        private void SetBonusHealth()
        {
            BonusHealth = _upgradesParams.GetValueByLevel<SpeedUpgradesData>(UpgradesLevel.SpeedLevel);
        }
        private void SetSpeed()
        {
            BonusSpeed = _upgradesParams.GetValueByLevel<SpeedUpgradesData>(UpgradesLevel.SpeedLevel);
        }

        private void SetHeightJump()
        {
            BonusHeightJump = _upgradesParams.GetValueByLevel<JumpUpgradesData>(UpgradesLevel.JumpHeightLevel);
        }
        
        private void SetAttack()
        {
            BonusAttack = Convert.ToInt32(_upgradesParams.GetValueByLevel<AttackUpgradesData>(UpgradesLevel.AttackLevel));
        }

    }

    [Serializable]
    public class HeroUpgradesLevelData
    {
        public int SpeedLevel;
        public int JumpHeightLevel;
        public int AttackLevel;
        public int HealthLevel;
    }
}