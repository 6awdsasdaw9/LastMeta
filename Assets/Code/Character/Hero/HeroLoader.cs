using System.Linq;
using Code.Data.Configs;
using Code.Data.GameData;
using Code.Data.ProgressData;
using Code.Debugers;
using Code.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Logger = Code.Debugers.Logger;

namespace Code.Character.Hero
{
    public class HeroLoader : MonoBehaviour, ISavedData
    {
        private IHero _hero;
        private SpawnPointsConfig _spawnPointsConfig;

        [Inject]
        private void Construct(SavedDataCollection savedDataCollection, SpawnPointsConfig spawnPointsConfig)
        {
            _spawnPointsConfig = spawnPointsConfig;
            savedDataCollection.Add(this);
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
            _hero.Health.Set(savedData.HeroHealth);
        }

        private void HealthSaveData(SavedData savedData)
        {
        
            Logger.ColorLog($"s {savedData.HeroHealth == null} h {_hero.Health == null}",ColorType.Red);
            Logger.ColorLog($"s {savedData.HeroHealth.CurrentHP} h {_hero.Health.Current}",ColorType.Red);
           
            savedData.HeroHealth.CurrentHP = _hero.Health.Current;
            savedData.HeroHealth.MaxHP = _hero.Health.Max;
        }

        private void MovementLoadData(SavedData savedData)
        {
            /*if (!savedData.HeroPosition.positionInScene.ContainsKey(CurrentLevel()))
                return;

            Vector3Data savedPosition = savedData.HeroPosition.positionInScene[CurrentLevel()];
            transform.position = savedPosition.AsUnityVector();*/
            Logger.ColorLog("Movement load data", ColorType.Green);
            
            if (savedData.SceneSpawnPoints.ContainsKey(CurrentLevel()))
            {
                var points = _spawnPointsConfig.SceneSpawnPoints
                    .FirstOrDefault(p => p.Scene.ToString() == CurrentLevel())
                    ?.Points;
                
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
            _hero.Upgrade.Init(savedData.HeroUpgradesLevel);
        }

        private void UpgradesSaveData(SavedData savedData) =>
            savedData.HeroUpgradesLevel = _hero.Upgrade.UpgradesLevel;

        private string CurrentLevel() =>
            SceneManager.GetActiveScene().name;
    }
}