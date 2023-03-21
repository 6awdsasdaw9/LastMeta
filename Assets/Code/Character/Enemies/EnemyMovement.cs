using Code.Character.Common;
using Code.Character.Hero;
using Code.Services;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Code.Character.Enemies
{
    public class EnemyMovement : Follow
    {
        [SerializeField] private EnemyAttack _attack;
        [SerializeField] private NavMeshAgent _agent;
        
        private Transform _heroTransform;
        private MovementLimiter _limiter;
        
        private float _minimalDistance;
        
        [Inject]
        private void Construct(HeroMovement hero,MovementLimiter limiter)
        {
            _heroTransform = hero.transform;
            _limiter = limiter;

            _minimalDistance = _agent.stoppingDistance;
        }
        
        private void Update()
        {
            if (HeroNotReached() && !_attack.attackIsActive && _limiter.charactersCanMove)
            {
                _agent.destination = _heroTransform.position;
            }
        }
        
        private bool HeroNotReached() => 
            Vector3.Distance(_agent.transform.position, _heroTransform.position) >= _minimalDistance;
    }
}