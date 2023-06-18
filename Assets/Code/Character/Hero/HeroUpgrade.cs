using System;
using System.Linq;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Code.Data.GameData;
using Code.Services.SaveServices;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using HeroConfig = Code.Data.Configs.HeroConfig;

namespace Code.Character.Hero
{
    public class HeroUpgrade : MonoBehaviour,  IHeroUpgrade
    {
        private HeroParams _heroParams;
        public HeroUpgradesLevelData UpgradesLevelUpgradesLevel => _upgradesLevelData;
        private HeroUpgradesLevelData _upgradesLevelData = new HeroUpgradesLevelData();
        
        public float BonusSpeed { get; private set; }
        public float BonusHeightJump { get; private set; }
        public int BonusAirJump { get; private set; }

        [Inject]
        private void Construct(HeroConfig heroConfig)
        {
            _heroParams = heroConfig.HeroParams;
        }

        public void Init(HeroUpgradesLevelData heroUpgradesLevelData)
        {
            _upgradesLevelData = heroUpgradesLevelData;
            SetSpeed();
            SetHeightJump();
            SetAirJump();
        }

        [Button]
        public void LevelUpSpeed()
        {
            _upgradesLevelData.SpeedLevel++;
            SetSpeed();
        }
        
        [Button]
        public void LevelUpJump()
        {
            _upgradesLevelData.JumpHeightLevel++;
            SetHeightJump();
        }

        [Button]
        public void LevelUpMaxAirJump()
        {
            _upgradesLevelData.AirJumpLevel++;
            SetAirJump();
        }

        private void SetSpeed()
        {
            BonusSpeed = GetUpgradeParam(UpgradeParamType.Speed, _upgradesLevelData.SpeedLevel);
        }

        private void SetHeightJump()
        {
            BonusHeightJump = GetUpgradeParam(UpgradeParamType.JumpHeight, _upgradesLevelData.JumpHeightLevel);
        }
        
        private void SetAirJump()
        {
            BonusAirJump = Convert.ToInt32(GetUpgradeParam(UpgradeParamType.AirJump, _upgradesLevelData.AirJumpLevel));
        }

        private float GetUpgradeParam(UpgradeParamType paramType, int level)
        {
            var values = _heroParams?.UpgradeParams
                .FirstOrDefault(s => s.Type == paramType)
                ?.Values;

            if (values == null)
                return 0;

            if (level > values.Count - 1)
                level = values.Count - 1;

            return values[level];
        }
        
    }

    [Serializable]
    public class HeroUpgradesLevelData
    {
        public int SpeedLevel;
        public int JumpHeightLevel;
        public int AirJumpLevel;
    }
}