using System.Linq;
using Code.Character.Hero.HeroInterfaces;
using Code.Data;
using Code.Data.Configs;
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
            savedDataStorage.Add(this);
            _hero = GetComponent<IHero>();
        }

        public void LoadData(SavedData savedData)
        {
            HealthLoadData(savedData);
            MovementLoadData(savedData);
            UpgradesLoadData(savedData);
        }

        public void SaveData(SavedData savedData)
        {
            HealthSaveData(savedData);
            MovementSaveData(savedData);
            UpgradesSaveData(savedData);
        }

        private void HealthLoadData(SavedData savedData)
        {
            if(_hero.Mode == Constants.GameMode.Real)
                return;
            
            _hero.Health.Set(savedData.HeroHealth);
        }

        private void HealthSaveData(SavedData savedData)
        {
            if(_hero.Mode == Constants.GameMode.Real)
                return;
            
            savedData.HeroHealth.CurrentHP = _hero.Health.Current;
            savedData.HeroHealth.MaxHP = _hero.Health.Max;
        }

        private void MovementLoadData(SavedData savedData)
        {
            if (savedData.SceneSpawnPoints.ContainsKey(CurrentLevel()))
            {
                var points = _scenesConfig.GetSceneParam(CurrentLevel())?.Points;
                var point = points?.FirstOrDefault(p => p.ID == savedData.SceneSpawnPoints[CurrentLevel()].ID);
                
                if (point == null)
                    return;
                
                transform.position = point.Position;
            }
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

        private void UpgradesLoadData(SavedData savedData)
        {
            if(_hero.Mode == Constants.GameMode.Real)
                return;
            
            _hero.Upgrade.Init(savedData.HeroUpgradesLevel);
        }

        private void UpgradesSaveData(SavedData savedData)
        {
            if(_hero.Mode == Constants.GameMode.Real)
                return;
            
            savedData.HeroUpgradesLevel = _hero.Upgrade?.UpgradesLevel;
        }

        private string CurrentLevel() =>
            SceneManager.GetActiveScene().name;
    }
}