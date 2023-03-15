using Code.Character.Hero;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Code.Character.Enemies
{
    public class AgentMoveToHero : Follow
    {
        [SerializeField] private NavMeshAgent _agent;
        private Transform _heroTransform;
        private MovementLimiter _limiter;
        
        private const float minimalDistance = 1;
        
        [Inject]
        private void Construct(HeroMovement hero,MovementLimiter limiter)
        {
            _heroTransform = hero.transform;
            _limiter = limiter;
        }
        
        private void Update()
        {
            if (_heroTransform && HeroNotReached() && _limiter.charactersCanMove)
            {
                _agent.destination = _heroTransform.position;
                
            }
        }

     

        private bool HeroNotReached() => 
            Vector3.Distance(_agent.transform.position, _heroTransform.position) >= minimalDistance;
    }
}