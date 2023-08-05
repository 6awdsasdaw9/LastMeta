using System;
using Code.Character.Hero.HeroInterfaces;
using Code.Debugers;
using Code.Logic.Collisions.Triggers;
using Code.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Logic.Objects
{
    public class DamageArea : MonoBehaviour
    {
        [SerializeField, Range(0,5)] private float _damage;
        [SerializeField,Range(0,5)] private float _pushPower;
        [SerializeField, Range(0,5)] private float _pushDuration;
        
        [Space,SerializeField] private Cooldown _cooldown;
        [SerializeField] private TriggerObserver _trigger;

        private IHero _hero;

        [Inject]
        private void Construct(IHero hero)
        {
            _hero = hero;
        }
    
        private void OnEnable()
        {
            SubscribeToEvent(true);
        }

        private void OnDisable()
        {
            SubscribeToEvent(false);
        }
        
        private void SubscribeToEvent(bool flag)
        {
            if (_trigger == null)
                return;

            if (flag)
            {
                _trigger.OnEnter+= OnOnEnter;
            }
            else
            {
                _trigger.OnEnter -= OnOnEnter;
            }
          
        }

        private void OnOnEnter(Collider obj)
        {
            TakeDamage();
        }

        private void TakeDamage()
        {
            if(!_cooldown.IsUp())
                return;
        
            _hero.Health.TakeDamage(_damage);
            _hero.Movement.SetSupportVelocity(-Direction() * _pushPower);
        
            ResetHeroSupportVelocity().Forget();
        }


        private async UniTaskVoid ResetHeroSupportVelocity()
        {
            await UniTask.Delay(
                TimeSpan.FromSeconds(_pushDuration * 0.69f),
                cancellationToken: gameObject.GetCancellationTokenOnDestroy());
            _hero.Movement.SetSupportVelocity(-Direction() * (_pushPower * 0.2f));

            await UniTask.Delay(
                TimeSpan.FromSeconds(_pushDuration * 0.31f),
                cancellationToken: gameObject.GetCancellationTokenOnDestroy());
            _hero.Movement.SetSupportVelocity(Vector2.zero);
        }

        private Vector3 Direction() => 
            (transform.position - _hero.Transform.position).normalized;

    }
}