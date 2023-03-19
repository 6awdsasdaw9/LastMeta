using System;
using Code.Character.Interfaces;
using Code.Data.GameData;
using Code.Data.States;
using Code.Debugers;
using Code.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroAttack : MonoBehaviour
    {
        [SerializeField] private HeroAnimator _animator;

        private InputService _inputService;
        private PowerData _power;
        private Collider[] _hits = new Collider[3];
        private int _layerMask;


        [Inject]
        private void Construct(InputService inputService, ConfigData configData)
        {
            _inputService = inputService;
            _inputService.PlayerAttackEvent += Attack;
            _power = configData.heroConfig.power;
            _layerMask = 1 << LayerMask.NameToLayer(Constants.HittableLayer);
        }

        private void OnDestroy()
        {
            _inputService.PlayerAttackEvent -= Attack;
        }

        private void Attack()
        {
            _animator.PlayAttack();
        }
        
        public void OnAttack()
        {
            PhysicsDebug.DrawDebug(StartPoint() + transform.forward, _power.damagedRadius, 1.0f);
            
            for (int i = 0; i < Hit(); ++i)
            {
                _hits[i].GetComponentInParent<IHealth>().TakeDamage(_power.damage);
            }
        }


        /*
        public void LoadProgress(PlayerProgress progress) =>
            _stats = progress.heroStats;*/

        private int Hit() =>
            Physics.OverlapSphereNonAlloc(StartPoint() + transform.localScale ,_power.damagedRadius, _hits, _layerMask);

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x + transform.localScale.x * 0.5f, transform.position.y + 1f, transform.position.z);
    }
}