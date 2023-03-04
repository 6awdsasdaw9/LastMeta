using Code.Character.Hero;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Code.Character.Enemies
{
    public class AgentMoveToHero : Follow
    {
        public NavMeshAgent agent;
        private const float minimalDistance = 1;
        private Transform _heroTransform;
        
        [Inject]
        private void Construct(HeroMovement hero)
        {
            _heroTransform = hero.transform;
        }
        
        private void Update()
        {
            if(_heroTransform && HeroNotReached())
                agent.destination = _heroTransform.position;
        }

        private bool HeroNotReached()
        {
            return Vector3.Distance(agent.transform.position, _heroTransform.position) >= minimalDistance;
        }
    }
}