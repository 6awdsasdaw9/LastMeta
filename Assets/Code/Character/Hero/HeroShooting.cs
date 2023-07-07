using System;
using System.Threading;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Code.Infrastructure.Factories;
using Code.Services;
using Code.Services.Input;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroShooting: MonoBehaviour, IHeroRangeAttack
    {
        public bool IsAttack { get; private set;}
        public ShootingParams ShootingParams { get; private set; }
        
        private IHero _hero;
        private InputService _inputService;
        private Cooldown _attackCooldown;
        private CancellationTokenSource _cts;
        private MissilesFactory _missilesFactory;
        
        private bool _isCanAttack => !_hero.Stats.IsDash 
                                    && !_hero.Stats.IsCrouch 
                                    && !_hero.Stats.IsJump
                                    && !_hero.Stats.IsBlockMove;
        [Inject]
        private void Construct(InputService inputService, HeroConfig heroConfig, MissilesFactory missilesFactory)
        {
            _hero = GetComponent<IHero>();
            _inputService = inputService;
            _missilesFactory = missilesFactory;
        }

        public void SetShootingParams(ShootingParams shootingParams)
        {
            ShootingParams = shootingParams;
        }
       
        public void Enable()
        {
            if(this != null) enabled = true;
        }

        private void OnEnable()
        {
            _inputService.OnPressAttackButton += StartAttack;
            _inputService.OnUnPressAttackButton += StopAttack;
        }

        public void Disable()
        {
            if(this != null) enabled = false;
        }

        private void OnDisable()
        {
            _inputService.OnPressAttackButton -= StartAttack;
            _inputService.OnUnPressAttackButton -= StopAttack;
        }

        public void StartAttack()
        {
            if (IsAttack || !_isCanAttack)
                return;

            IsAttack = true;
            _hero.Animator.PlayAttack();
            _hero.Movement.BlockMovement();

            StartAttackCycle().Forget();
        }

        private async  UniTaskVoid StartAttackCycle()
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: _cts.Token);
            
            _attackCooldown = new Cooldown();
            _attackCooldown.SetTime(ShootingParams.AttackCooldown);
            _attackCooldown.ResetCooldown();
            
            while (IsAttack)
            {
                var missile = _missilesFactory.SpawnMissile(ShootingParams,_hero);
                missile.Movement.StartMove();
                await UniTask.WaitUntil(_attackCooldown.UpdateCooldown, cancellationToken: _cts.Token);
                _attackCooldown.ResetCooldown();
            }
        }

        public void StopAttack()
        {
            if(!IsAttack)
                return;
            _cts?.Cancel();
            IsAttack = false;
            _hero.Animator.PlayStopAttack();
            _hero.Movement.UnBlockMovement();
        }
        
       
    }
}