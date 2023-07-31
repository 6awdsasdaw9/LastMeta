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
        [SerializeField] private EnemyAttack _attack;
        [SerializeField] private NavMeshAgent _agent;
        
        private Transform _heroTransform;
        private MovementLimiter _limiter;
        
        private float _minimalDistance;
        
        [Inject]
        private void Construct(IHero hero,MovementLimiter limiter)
        {
            _heroTransform = hero.Transform;
            _limiter = limiter;

            _minimalDistance = _agent.stoppingDistance;
        }
        
        private void Update()
        {
            if (HeroNotReached() && !_attack.IsActive && _limiter.CharactersCanMove)
            {
                _agent.destination = _heroTransform.position;
            }
        }
        
        private bool HeroNotReached() => 
            Vector3.Distance(_agent.transform.position, _heroTransform.position) >= _minimalDistance;
    }
}