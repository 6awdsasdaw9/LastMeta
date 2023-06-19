using System;
using System.Threading;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Code.Services;
using Code.Services.Input;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Character.Hero
{
    public class HeroDashAbility : Ability
    {
        private readonly InputService _inputService;
        private readonly IHero _hero;
        private Data _currentData;
        
        private CancellationTokenSource _durationCts;
        private CancellationTokenSource _abilityCts;
        
        private readonly Cooldown _durationCooldown;
        private readonly Cooldown _abilityCooldown;
        private bool _isCanDash => !_hero.Stats.IsAttack 
                                   && !_hero.Stats.IsCrouch
                                   && !_hero.Stats.IsBlockMove;
        public bool IsDash { get; private set; }

        public HeroDashAbility(IHero hero, InputService inputService)
        {
            Type = HeroAbilityType.Dash;

            _hero = hero;
            _inputService = inputService;
            
            _durationCooldown = new Cooldown();
            _abilityCooldown = new Cooldown();
        }

        public override void OpenAbility()
        {
            if (IsOpen)
                return;

            IsOpen = true;
            _inputService.OnPressDash += StartApplying;
        }

        public void SetData(Data data)
        {
            _currentData = data;
            _durationCooldown.SetTime(_currentData.Duration);
            _abilityCooldown.SetTime(_currentData.Cooldown);
        }

        public override void StartApplying()
        {
            if(!_abilityCooldown.IsUp() || !_isCanDash)
                return;

            IsDash = true;
            _hero.Movement.BlockMovement();
            _hero.Movement.SetBonusSpeed(_currentData.SpeedBonus);
            _hero.Animator.PlayDash(true);
            UpdateDurationCooldown().Forget();
        }


        private async UniTaskVoid UpdateDurationCooldown()
        {
            _durationCts?.Cancel();
            _durationCts = new CancellationTokenSource();
            
            _durationCooldown.ResetCooldown();

            var value = _currentData.SpeedBonus;
            _hero.Movement.SetBonusSpeed(value);

            var heroForward = _hero.Transform.localScale.x;
         
            while (!_durationCooldown.UpdateCooldown())
            {
                if (_durationCooldown.Normalize < 0.3f)
                {
                    var sec = Time.deltaTime;
                    value -= sec;
                    _hero.Movement.SetBonusSpeed(value);
                }
                if (_hero.Transform.localScale.x == -_inputService.GetDirection())
                {
                    StopApplying();
                    break;
                }
                
                await UniTask.DelayFrame(1, cancellationToken: _durationCts.Token);
            }
            
            StopApplying();
            UpdateAbilityCooldown().Forget();
        }

        private async UniTaskVoid UpdateAbilityCooldown()
        {
            _abilityCts?.Cancel();
            _abilityCts = new CancellationTokenSource();
            await UniTask.WaitUntil(_abilityCooldown.IsUp, cancellationToken: _abilityCts.Token);
        }

        public override void StopApplying()
        {
            IsDash = false;
            _durationCts?.Cancel();
            _abilityCts?.Cancel();
            
            if (_hero.Transform.gameObject == null)
                return;

            _hero.Animator.PlayDash(false);
            _hero.Movement.SetBonusSpeed(0);
            _hero.Movement.UnBlockMovement();
        }

        [Serializable]
        public class Data : AbilitySettings
        {
            public float Cooldown;
            public float SpeedBonus;
            public float Duration;
        }
    }
}