using System;
using Code.Character.Hero.HeroInterfaces;
using Code.Logic.Triggers;
using Code.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Logic.Objects
{
    public class Trampoline : MonoBehaviour, IEventSubscriber
    {
        [SerializeField] private TriggerObserver _trigger;
        [SerializeField] private Vector2 _force = Vector2.up;
        [SerializeField] private float _duration = 1;
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

        public void SubscribeToEvent(bool flag)
        {
            /*if (flag)
            {
                _trigger.OnEnter += OnEnter;
            }
            else
            {
                
                _trigger.OnEnter -= OnEnter;
            }*/
        }

        /*
        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Rigidbody rb))
            {
                if(rb.velocity.y > 0.2f ) rb.AddForce(_force, ForceMode.Impulse);
            }
        }
        */

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Rigidbody rb))
            {
                    rb.AddForce(_force, ForceMode.Impulse);
            }
        }

        private void OnEnter(Collider obj)
        {
            if (obj.TryGetComponent(out Rigidbody rb))
            {
                rb.AddForce(_force, ForceMode.Impulse);
            }
        }

        private async UniTaskVoid PushHero()
        {
            _hero.Movement.SetSupportVelocity(_force);
            await UniTask.Delay(TimeSpan.FromSeconds(_duration));
            _hero.Movement.SetSupportVelocity(Vector2.zero);
        }
    }
}