using System.Linq;
using Code.Character.Hero.HeroInterfaces;
using Code.Data;
using Code.Data.AdditionalData;
using Code.Data.Configs;
using Code.Debugers;
using Code.Services;
using Code.Services.SaveServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroLoader : MonoBehaviour, ISavedData
    {
        private IHero _hero;
        private ScenesConfig _scenesConfig;

        [Inject]
        private void Construct(SavedDataStorage savedDataStorage, ScenesConfig scenesConfig)
        {
            _scenesConfig = scenesConfig;
            _hero = GetComponent<IHero>();
            savedDataStorage.Add(this);
        }

        public void LoadData(SavedData savedData)
        {
            MovementLoadData(savedData);
            
            if(_hero.GameMode == Constants.GameMode.Real)
                return;
            
            HealthLoadData(savedData);
            UpgradesLoadData(savedData);
            AbilityLoadData(savedData);
        }

        public void SaveData(SavedData savedData)
        {
            MovementSaveData(savedData);
            
            if(_hero.GameMode == Constants.GameMode.Real)
                return;
            
            HealthSaveData(savedData);
            UpgradesSaveData(savedData);
            AbilitySaveData(savedData);
        }

        private void AbilityLoadData(SavedData savedData)
        {
            _hero.Ability.Init(savedData.HeroAbilityLevel);
        }

        private void AbilitySaveData(SavedData savedData)
        {
            savedData.HeroAbilityLevel = _hero.Ability.AbilityLevelData;
        }

        private void HealthLoadData(SavedData savedData)
        {
            _hero.Health.Set(savedData.HeroHealth);
        }

        private void HealthSaveData(SavedData savedData)
        {
            savedData.HeroHealth.CurrentHP = _hero.Health.Current;
            savedData.HeroHealth.MaxHP = _hero.Health.Max;
        }

        private void MovementLoadData(SavedData savedData)
        {
            if (!savedData.SceneSpawnPoints.ContainsKey(CurrentLevel())) return;
            
            var points = _scenesConfig.GetSceneParam(CurrentLevel())?.Points;
            var point = points?.FirstOrDefault(p => p.ID == savedData.SceneSpawnPoints[CurrentLevel()].ID);
            if (point == null) return;
            
            transform.position = point.Position;
        }

        private void MovementSaveData(SavedData savedData)
        {
            if (savedData.HeroPosition.positionInScene.ContainsKey(CurrentLevel()))
            {
                savedData.HeroPosition.positionInScene[CurrentLevel()] = transform.position.AsVectorData();
            }
            else
            {
                savedData.HeroPosition.AddPosition(CurrentLevel(), transform.position.AsVectorData());
            }
        }

        private void UpgradesLoadData(SavedData savedData) => _hero.Upgrade.Init(savedData.HeroUpgradesLevel);

        private void UpgradesSaveData(SavedData savedData) => savedData.HeroUpgradesLevel = _hero.Upgrade?.UpgradesLevel;

        private string CurrentLevel() => SceneManager.GetActiveScene().name;
    }
}