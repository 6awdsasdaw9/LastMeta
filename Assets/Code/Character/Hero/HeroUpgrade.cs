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
    public class HeroUpgrade: MonoBehaviour, ISavedData
    {
        [Header("Components")] 
        [SerializeField] private HeroMovement _heroMovement;
        [SerializeField] private HeroJump _heroJump;
        
        private HeroConfig _heroConfig;
        private int _speedLevel;
        private int _jumpLevel;

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
            _heroMovement.SetUpgradeSpeed(GetUpgradeParam(UpgradeParamType.Speed,_speedLevel));
        }

        private float GetUpgradeParam(UpgradeParamType paramType, int level)
        {
            //TODO Переделать в айди
            return _heroConfig.UpgradeParams
                .FirstOrDefault(s => s.Type == paramType)?.Params.FirstOrDefault(p => p.Lvl == level)?.Value ?? 0;
        }

        public void LoadData(SavedData savedData)
        {
            
        }

        public void SaveData(SavedData savedData)
        {
            
        }
    }
}