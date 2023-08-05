using Code.Character.Hero.HeroInterfaces;
using Code.Logic.Triggers;
using Code.Services;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Code.Character.Enemies
{
    public class EnemyMovementToHero: FollowTriggerObserver
    {
        [SerializeField] private EnemyMelleAttack melleAttack;
        [SerializeField] private NavMeshAgent _agent;
        
        private Transform _heroTransform;
        
        private float _minimalDistance;
        private float _speed;
        
        public void Init(Transform heroTransform, float speed)
        {
            _heroTransform = heroTransform;
            _speed = speed;
            _minimalDistance = _agent.stoppingDistance;
        }
        
        private void Update()
        {
            if (HeroNotReached() && !melleAttack.IsActive )
            {
                _agent.destination = _heroTransform.position;
            }
        }
        
        private bool HeroNotReached() => 
            Vector3.Distance(_agent.transform.position, _heroTransform.position) >= _minimalDistance;

        public override void EnableComponent()
        {
            _agent.speed = _speed;
            base.EnableComponent();
        }
    }
}