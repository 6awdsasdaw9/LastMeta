using System;
using System.Linq;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Code.Data.GameData;
using UnityEngine;
using Zenject;

namespace Code.Logic.Objects.Spikes
{
    public class MissileSpikeController: MonoBehaviour
    {
        [SerializeField] private SpikeType _type;
        [SerializeField] private SpikeController _spikeController;
        
        public void Init(IHero hero, ObjectsConfig objectsConfig)
        {
            var data = objectsConfig.SpikesData.FirstOrDefault(s => s.Type == _type);
            _spikeController.Init(owner: transform, hero: hero, data: data);
        }

        private void OnEnable()
        {
            _spikeController.SetEndReaction();
        }

        public  void StartReaction(Vector3 position)
        {
            transform.position = position - Vector3.up * 0.1f;
            _spikeController.StartReaction();
        }

        public  void EndReaction()
        {
            _spikeController.EndReaction();
        }
        
    }
}