using System;
using Code.Character.Enemies.EnemiesInterfaces;
using Code.Debugers;
using Code.Logic.Collisions.Triggers;
using Code.Services;
using Code.Services.PauseListeners;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Character.Enemies
{
    public class EnemyMovementPatrol : FollowTriggerObserver, IPauseListener
    {
        [SerializeField] private float _patrolDistance = 1;
        [SerializeField] private NavMeshAgent _agent;

        private Cooldown _cooldown;
        private float _speed;
        private float _minimalDistance;
        private Vector3 _startPoint, _finishPoint, _targetPoint;
        private IEnemyStats _enemyStats;
        public bool IsMoving { get; private set; }

        public void Init(float speed, float cooldown, IEnemyStats enemyStats)
        {
            _speed = speed;
            _cooldown = new Cooldown();
            _cooldown.SetMaxTime(cooldown);
            _enemyStats = enemyStats;
        }

        private void Awake()
        {
            if (_agent == null)
            {
                Logg.ColorLog($"EnemyMovementPatrol from {gameObject.name} has not Nav Mesh Agent",LogStyle.Warning);
            }
            _minimalDistance = _agent.stoppingDistance;
            InitPatrolPoints();
        }

        private void Update()
        {
            if (_enemyStats is { IsBlock: false })
            {
                MoveToTarget();
            }
        }

        private void InitPatrolPoints()
        {
            _startPoint = transform.position;
            _finishPoint = new Vector3(_startPoint.x + _patrolDistance, _startPoint.y, _startPoint.z);
            _targetPoint = _finishPoint;
        }

        private void MoveToTarget()
        {
          //  if(!IsMoving)return;
            if (PointNotReached(_targetPoint))
            {
                _agent.destination = _targetPoint;
                Logg.ColorLog($"{gameObject.name} _agent.destination = _targetPoint", ColorType.Blue);
            }
            if(Mathf.Abs(_agent.velocity.x) == 0 && _cooldown.IsUp())
            {
                Logg.ColorLog($"{gameObject.name} SwitchTarget {_targetPoint}", ColorType.Blue);
                SwitchTarget();
                _cooldown.SetMaxCooldown();
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
            Logg.ColorLog("Enable Component");
            IsMoving = true;
            _agent.speed = _speed;
            _cooldown.SetMaxCooldown();
            base.EnableComponent();
        }

        public override void DisableComponent()
        {
            Logg.ColorLog("Disable Component");
            IsMoving = false;
            _targetPoint = transform.position;
            _agent.destination = _targetPoint;
            base.DisableComponent();
            base.DisableComponent();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawRay(transform.position, Vector3.right * _patrolDistance); 
        }

        public void OnPause()
        {
            _agent.destination = _targetPoint;
            _targetPoint = transform.position;
        }

        public void OnResume()
        {
        }

        private void OnValidate()
        {
            _agent = GetComponent<NavMeshAgent>();  
        }
    }
}