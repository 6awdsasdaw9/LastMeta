using System;
using Code.Character.Interfaces;
using Code.Data.GameData;
using Code.Data.States;
using Code.Debugers;
using Code.Services;
using Code.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroAttack : MonoBehaviour
    {
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private HeroMovement _movement;

        private MovementLimiter _movementLimiter;
        private InputService _inputService;
        private PowerData _power;

        private readonly Collider[] _hits = new Collider[7];
        private bool _attackIsActive;
        private int _layerMask;

        [Inject]
        private void Construct(MovementLimiter movementLimiter, InputService inputService, ConfigData configData)
        {
            _movementLimiter = movementLimiter;
            _inputService = inputService;
            _power = configData.heroConfig.power;
        }

        private void OnEnable()
        {
            _inputService.PlayerAttackEvent += Attack;
            _layerMask = 1 << LayerMask.NameToLayer(Constants.HittableLayer);
        }

        private void OnDisable()
        {
            _inputService.PlayerAttackEvent -= Attack;
        }

        private void Attack()
        {
            if (_attackIsActive)
                return;

            _attackIsActive = true;
            _animator.PlayAttack();
            _movement.BlockMovement();
        }

        /// <summary>
        /// Animation Event
        /// </summary>
        private void OnAttack()
        {
            PhysicsDebug.DrawDebug(StartPoint(), _power.damagedRadius, 1.0f);
            for (int i = 0; i < Hit(); ++i)
                _hits[i].GetComponent<IHealth>().TakeDamage(_power.damage);
        }

        /// <summary>
        /// Animation Event
        /// </summary>
        private void OnAttackEnded()
        {
            _attackIsActive = false;
            _movement.UnBlockMovement();
        }

        private int Hit() =>
            Physics.OverlapSphereNonAlloc(StartPoint(), _power.damagedRadius, _hits, _layerMask);

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x + transform.localScale.x * 0.1f, transform.position.y + 0.7f,
                transform.position.z);
    }
}