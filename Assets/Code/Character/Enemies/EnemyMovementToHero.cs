using Code.Logic.Collisions.Triggers;
using Code.Services.PauseListeners;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Character.Enemies
{
    public class EnemyMovementToHero : FollowTriggerObserver, IPauseListener
    {
        [SerializeField] private NavMeshAgent _agent;

        private Transform _heroTransform;
        private Vector3 _target;

        private float _minimalDistance;
        private float _speed;
        public bool IsMoving { get; private set; }

        public void Init(Transform heroTransform, float speed)
        {
            _heroTransform = heroTransform;
            _speed = speed;
            _minimalDistance = _agent.stoppingDistance;
        }

        private void Update()
        {
            if (TargetNotReached())
            {
                _agent.destination =_target;
            }
        }

        private bool TargetNotReached()
        {
            return Vector3.Distance(_agent.transform.position, _target) >= _minimalDistance;
        }

        public override void EnableComponent()
        {
            _agent.speed = _speed;
            IsMoving = true;
            base.EnableComponent();
        }

        public override void DisableComponent()
        {
            IsMoving = false;
            base.DisableComponent();
        }

        public void OnPause()
        {
            _target = transform.position;
        }

        public void OnResume()
        {
            _target = _heroTransform.position;
        }
    }
}