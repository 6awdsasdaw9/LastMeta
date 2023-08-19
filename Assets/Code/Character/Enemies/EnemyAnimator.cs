using System;
using Code.Debugers;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Character.Enemies
{
    public class EnemyAnimator : MonoBehaviour
    {
        public EnemyAnimationEvents Events => _animationEvents;
        [SerializeField] private EnemyAnimationEvents _animationEvents;
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _agent;

        private static readonly int _speed_f = Animator.StringToHash("Speed");
        private static readonly int _melleAttack_t = Animator.StringToHash("MelleAttack");
        private static readonly int _melleAttackSpeed_f = Animator.StringToHash("MelleAttackSpeed");
        private static readonly int _rangeAttack_t = Animator.StringToHash("RangeAttack");
        private static readonly int _rangeAttackSpeed_f = Animator.StringToHash("RangeAttackSpeed");
        private static readonly int _death_t = Animator.StringToHash("Die");
        private static readonly int _enter_t = Animator.StringToHash("Enter");
        private static readonly int _disable_t = Animator.StringToHash("Disable");

        public event Action OnEntet;

        private void OnEnable()
        {
            Events.OnEnter += OnEnter;
        }

        private void OnEnter()
        {
            OnEntet?.Invoke();
        }

        private void Update()
        {
            PlayMove(Mathf.Abs(_agent.velocity.x));
        }

        public void PlayEnter(Action onEnter = null)
        {
            OnEntet = onEnter;
            Logg.ColorLog($"{gameObject.name} PlayEnter");
            _animator.SetTrigger(_enter_t);
        }

        public void PlayDisable() => _animator.SetTrigger(_disable_t);
        public void PlayDeath() => _animator.SetTrigger(_death_t);
        public void PlayMelleAttack() => _animator.SetTrigger(_melleAttack_t);
        public void SetMelleAttackAnimationSpeed(float value) => _animator.SetFloat(_melleAttackSpeed_f, value);
        public void PlayRangeAttack() => _animator.SetTrigger(_rangeAttack_t);
        public void SetRangeAttackAnimationSpeed(float value) => _animator.SetFloat(_rangeAttackSpeed_f, value);
        private void PlayMove(float speed) => _animator.SetFloat(_speed_f, speed);


        private void OnValidate()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<Animator>();
            _animationEvents = GetComponentInChildren<EnemyAnimationEvents>();
        }
    }
}