using Code.Logic.Triggers;
using Code.Services;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Character.Enemies
{
    public class EnemyMovementPatrol : FollowTriggerObserver
    {
        [SerializeField] private float _patrolDistance = 1;
        [SerializeField] private Cooldown _cooldown;
        [SerializeField] private NavMeshAgent _agent;
        
        private float _minimalDistance;
        private Vector3 _startPoint, _finishPoint, _targetPoint;
        
        private void Awake()
        {
            _minimalDistance = _agent.stoppingDistance;
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
            MoveToTarget();
        }
        
        private void MoveToTarget()
        {
            if (PointNotReached(_targetPoint))
            {
                _agent.destination = _targetPoint;
            }
            if(Mathf.Abs(_agent.velocity.x) == 0 && _cooldown.IsUp())
            {
                SwitchTarget();
                _cooldown.ResetCooldown();
            }
        }

        private void SwitchTarget()
        {
            _targetPoint = _targetPoint == _startPoint ? _finishPoint : _startPoint;
        }

        private bool PointNotReached(Vector3 point)
        {
            return Vector3.Distance(_agent.transform.position, point) >= _minimalDistance;
        }
        
        public override void EnableComponent()
        {
            _cooldown.ResetCooldown();
            base.EnableComponent();
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawRay(transform.position, Vector3.right * _patrolDistance); 
        }
    }
}