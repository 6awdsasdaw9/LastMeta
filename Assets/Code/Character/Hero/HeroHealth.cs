using System;
using Code.Character.Common;
using Code.Character.Interfaces;
using Code.Data.ProgressData;
using Code.Data.States;
using Code.Debugers;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroHealth : MonoBehaviour, IHealth, ISavedData
    {
        [SerializeField] private HeroAudio _heroAudio;
        [SerializeField] private SpriteVFX spriteVFX;
        private HealthData _healthData;
        public event Action HealthChanged;

        [Inject]
        private void Construct(SavedDataCollection savedDataCollection)
        {
            savedDataCollection.Add(this);
        }
        
        public float Current
        {
            get => _healthData.currentHP;
            set
            {
                if (_healthData.currentHP != value)
                {
                    _healthData.currentHP = value;
                }
            }
        }

        public float Max
        {
            get => _healthData.maxHP;
            set => _healthData.maxHP = value;
        }



        public void TakeDamage(float damage)
        {
            if (Current <= 0)
                return;

            _heroAudio.PlayDamageAudio();
            spriteVFX.RedColorize();
            
            Current -= damage;

            HealthChanged?.Invoke();
        }

        public void LoadData(SavedData savedData)
        {
            _healthData = savedData.heroHealth;
            Log.ColorLog("HP" + Current);
            
            HealthChanged?.Invoke();
        }

        public void SaveData(SavedData savedData)
        {
            savedData.heroHealth.currentHP = Current;
            savedData.heroHealth.maxHP = Max;
        }
    }
}