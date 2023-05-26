using Code.Data.GameData;
using Code.Data.ProgressData;
using Code.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroLoader : MonoBehaviour, ISavedData
    {
        private IHero _hero;

        [Inject]
        private void Construct(SavedDataCollection savedDataCollection)
        {
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
            _hero.Health.Set(savedData.HeroHealth.CurrentHP,savedData.HeroHealth.MaxHP);
        }

        private void HealthSaveData(SavedData savedData)
        {
            savedData.HeroHealth.CurrentHP = _hero.Health.Current;
            savedData.HeroHealth.MaxHP = _hero.Health.Max;
        }

        private void MovementLoadData(SavedData savedData)
        {
            if (!savedData.HeroPosition.positionInScene.ContainsKey(CurrentLevel()))
                return;

            Vector3Data savedPosition = savedData.HeroPosition.positionInScene[CurrentLevel()];
            transform.position = savedPosition.AsUnityVector();
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

        private void UpgradesLoadData(SavedData savedData) => 
            _hero.Upgrade.Init(savedData.HeroUpgradesLevel);

        private void UpgradesSaveData(SavedData savedData) => 
            savedData.HeroUpgradesLevel = _hero.Upgrade.UpgradesLevel;

        private string CurrentLevel() =>
            SceneManager.GetActiveScene().name;
    }
}