using UnityEngine;
using UnityEngine.AI;

namespace Code.Character.Enemies
{
    public class AgentMoveToHero : Follow
    {
        public NavMeshAgent agent;

        private const float minimalDistance = 1;

      //  private IGameFactory _gameFactory;
        private Transform _heroTransform;

        public void Construct(Transform heroTransform) => 
            _heroTransform = heroTransform;

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