using System;
using System.Linq;
using Code.Data.Configs;
using Code.Data.GameData;
using Code.Data.ProgressData;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using HeroConfig = Code.Data.Configs.HeroConfig;

namespace Code.Character.Hero
{
    public class HeroUpgrade : MonoBehaviour,  IHeroUpgrade
    {
        private Data.GameData.HeroConfig _heroConfig;
        
        public HeroUpgradesData UpgradesLevel => _upgradesData;
        private HeroUpgradesData _upgradesData = new HeroUpgradesData();
        
        public float BonusSpeed { get; private set; }
        public float BonusHeightJump { get; private set; }
        public int BonusAirJump { get; private set; }

        [Inject]
        private void Construct(HeroConfig heroConfig, SavedDataCollection dataCollection)
        {
            _heroConfig = heroConfig.heroConfig;
           // dataCollection.Add(this);
        }


        public void Init(HeroUpgradesData heroUpgradesData)
        {
            _upgradesData = heroUpgradesData;
            SetSpeed();
            SetHeightJump();
            SetAirJump();
        }

        [Button]
        public void LevelUpSpeed()
        {
            _upgradesData.SpeedLevel++;
            SetSpeed();
        }


        [Button]
        public void LevelUpJump()
        {
            _upgradesData.JumpHeightLevel++;
            SetHeightJump();
        }

        [Button]
        public void LevelUpMaxAirJump()
        {
            _upgradesData.AirJumpLevel++;
            SetAirJump();
        }

        private void SetSpeed()
        {
            BonusSpeed = GetUpgradeParam(UpgradeParamType.Speed, _upgradesData.SpeedLevel);
        }

        private void SetHeightJump()
        {
            BonusHeightJump = GetUpgradeParam(UpgradeParamType.JumpHeight, _upgradesData.JumpHeightLevel);
        }


        private void SetAirJump()
        {
            BonusAirJump = Convert.ToInt32(GetUpgradeParam(UpgradeParamType.AirJump, _upgradesData.AirJumpLevel));
        }

        private float GetUpgradeParam(UpgradeParamType paramType, int level)
        {
            var values = _heroConfig.UpgradeParams
                .FirstOrDefault(s => s.Type == paramType)
                ?.Values;

            if (values == null)
                return 0;

            if (level > values.Count - 1)
                level = values.Count - 1;

            return values[level];
        }

        /*public void LoadData(SavedData savedData)
        {
            _upgradesData = savedData.HeroUpgradesData;
            SetSpeed();
            SetHeightJump();
            SetAirJump();
        }

        public void SaveData(SavedData savedData)
        {
            savedData.HeroUpgradesData = _upgradesData;
        }*/
        
    }

    [Serializable]
    public class HeroUpgradesData
    {
        public int SpeedLevel;
        public int JumpHeightLevel;
        public int AirJumpLevel;
    }
}