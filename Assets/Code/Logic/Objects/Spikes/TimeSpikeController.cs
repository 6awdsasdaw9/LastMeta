using System.Linq;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Code.Data.GameData;
using Code.Logic.TimingObjects.TimeObserverses;
using UnityEngine;
using Zenject;

namespace Code.Logic.Objects.Spikes
{
    public class TimeSpikeController : TimeObserver
    {
        [SerializeField] private SpikeType _type;
        [SerializeField] private SpikeController _spikeController;

        [Inject]
        private void Construct(DiContainer container)
        {
            var data = container.Resolve<ObjectsConfig>().SpikesData.FirstOrDefault(s => s.Type == _type);
            var hero = container.Resolve<IHero>();
            _spikeController.Init(owner: transform, hero: hero, data: data);
        }

        protected override void StartReaction()
        {
            _spikeController.StartReaction();
        }
        
        protected override void EndReaction()
        {
            _spikeController.EndReaction();
        }

        protected override void SetStartReaction()
        {
            _spikeController.SetStartReaction();
        }

        protected override void SetEndReaction()
        {
            _spikeController.SetEndReaction();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            _spikeController.AttackHero();
        }
    }
}