using System;
using System.Threading;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs.HeroConfigs;
using Code.Services;
using Code.Services.EventsSubscribes;
using Code.Services.Input;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero.Abilities
{
    public class HeroDashAbility : Ability, IEventsSubscriber
    {
        private readonly InputService _inputService;
        private readonly IHero _hero;
        public Data Param { get; private set; }

        private CancellationTokenSource _durationCts;
        private CancellationTokenSource _abilityCts;

        private readonly Cooldown _durationCooldown;
        private readonly Cooldown _abilityCooldown;

        private bool _isCanDash => Param != null || IsDash
                                   && !_hero.Stats.IsAttack
                                   && !_hero.Stats.IsCrouch
                                   && !_hero.Stats.IsBlockMove;
        public bool IsDash { get; private set; }

        public HeroDashAbility(DiContainer container)
        {
            _hero = container.Resolve<IHero>();
            _inputService = container.Resolve<InputService>();

            Type = HeroAbilityType.Dash;
            _durationCooldown = new Cooldown();
            _abilityCooldown = new Cooldown();
            
            SubscribeToEvents(true);
        }

        public void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _inputService.OnPressDash += StartApplying;
            }
            else
            {
                _inputService.OnPressDash -= StartApplying;
            }
        }

        public void SetData(Data data, int level)
        {
            Level = level;
            if(!IsOpen) return;
            Param = data;
            _durationCooldown.SetMaxTime(Param.Duration);
            _abilityCooldown.SetMaxTime(Param.Cooldown);
        }

        public override void StartApplying()
        {
            if (!IsOpen || !_abilityCooldown.IsUp() || !_isCanDash || _hero?.Transform == null) return;

            IsDash = true;
            _hero.Animator?.PlayDash(true);
            UpdateDurationCooldown().Forget();
        }


        private async UniTaskVoid UpdateDurationCooldown()
        {
            if (_hero.Transform.gameObject == null) return;
            
            _durationCts?.Cancel();
            _durationCts = new CancellationTokenSource();
            _durationCooldown.SetMaxCooldown();
            
            var value = Param.SpeedBonus * 0.2f;
          // _hero.Movement.AddBonusSpeed(value);

            while (!_durationCooldown.IsUp())
            {
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

            if (_hero.Transform.gameObject == null) return;

            _hero.Animator.PlayDash(false);
           // _hero.Movement.AddBonusSpeed(-Param.SpeedBonus * 0.2f);
            _abilityCooldown.SetMaxCooldown();
       //     _hero.Movement.UnBlockMovement();
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