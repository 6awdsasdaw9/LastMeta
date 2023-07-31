using System;
using System.Diagnostics;
using Code.Character.Hero.HeroInterfaces;
using Code.Logic.Triggers;
using Code.Services;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Code.Character.Enemies
{
    public class EnemyMovement : FollowTriggerObserver
    {
        [SerializeField] private float _patrolDistance = 1;
        [SerializeField] private EnemyAttack _attack;
        [SerializeField] private NavMeshAgent _agent;
        
        private Transform _heroTransform;
        private MovementLimiter _limiter;
        
        private float _minimalDistance;
        private Vector3 _startPoint, _finishPoint, _targetPoint;
        

        [Inject]
        private void Construct(IHero hero,MovementLimiter limiter)
        {
            _heroTransform = hero.Transform;
            _limiter = limiter;

            _minimalDistance = _agent.stoppingDistance;
        }

        private void Awake()
        {
            InitPatrolPoints();
        }

        private void InitPatrolPoints()
        {
            _startPoint = transform.position;
            _finishPoint = new Vector3(_startPoint.x + _patrolDistance, _startPoint.y, _startPoint.z);
            _targetPoint = _finishPoint;
        }

        private void Update()
        {
            /*if (PointNotReached(_heroTransform.position) && !_attack.attackIsActive && _limiter.CharactersCanMove)
            {
                MoveToHero();
            }
            else
            {
                MoveToTarget();
            }*/
            MoveToTarget();
        }

        private void MoveToHero()
        {
            _agent.destination = _heroTransform.position;
        }

        private void MoveToTarget()
        {
            if (PointNotReached(_targetPoint))
            {
                _agent.destination = _targetPoint;
            }
            if(Mathf.Abs(_agent.velocity.x) == 0)
            {
                SwitchTarget();
            }
        }

        private void SwitchTarget()
        {
            if (_targetPoint == _startPoint)
            {
                _targetPoint = _finishPoint;
            }
            else
            {
                _targetPoint = _startPoint;
            }
        }

        private bool PointNotReached(Vector3 point) => 
            Vector3.Distance(_agent.transform.position, point) >= _minimalDistance;


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawRay(transform.position, Vector3.right * _patrolDistance); 
        }
    }
}