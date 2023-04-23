using System;
using System.Linq;
using Code.Data.Configs;
using Code.Data.ProgressData;
using Code.Data.States;
using Code.Services;
using Code.Services.Input;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroUpgrade : MonoBehaviour, ISavedData
    {
        [Header("Components")] 
        [SerializeField] private HeroMovement _heroMovement;
        [SerializeField] private HeroJump _heroJump;

        private HeroConfig _heroConfig;
        
        private int _speedLevel;
        private int _jumpLevel;
        private int _airJumpLevel;
        
        [Inject]
        private void Construct(InputService input, MovementLimiter limiter, GameConfig gameConfig, SavedDataCollection dataCollection)
        {
            _heroConfig = gameConfig.heroConfig;
            dataCollection.Add(this);
        }

        [Button] //!!!!!!!!!!!!!!!!!
        public void LevelUpSpeed()
        {
            _speedLevel++;
            _heroMovement.SetUpgradeSpeed(GetUpgradeParam(UpgradeParamType.Speed, _speedLevel));
        }     
        
        
        [Button] //!!!!!!!!!!!!!!!!!
        public void LevelUpJump()
        {
            _jumpLevel++;
            _heroJump.SetUpgradeJumpHeight(GetUpgradeParam(UpgradeParamType.JumpHeight, _jumpLevel));
        }

        [Button] //!!!!!!!!!!!!!!!!!
        public void LevelUpMaxAirJump()
        {
            _airJumpLevel++;
            _heroJump.SetMaxAirJump(Convert.ToInt32(GetUpgradeParam(UpgradeParamType.AirJump, _airJumpLevel)));
        }
        
        private float GetUpgradeParam(UpgradeParamType paramType, int level)
        {
            //TODO Переделать в айди
            var values = _heroConfig.UpgradeParams
                .FirstOrDefault(s => s.Type == paramType)
                ?.Values;

            if (values == null)
                return 0;
            
            if (level > values.Count - 1)
                level = values.Count - 1;
            
            return values[level];
        }

        public void LoadData(SavedData savedData)
        {
        }

        public void SaveData(SavedData savedData)
        {
        }
    }
    
    [Serializable]
    public class HeroUpgradesData
    {
        public int SpeedLevel;
        public int JumpHeightLevel;
        public int AirJumpLevel;
    }
}