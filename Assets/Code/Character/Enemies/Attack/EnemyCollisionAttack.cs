using System;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.GameData;
using Code.Logic.Collisions;
using Code.Services.EventsSubscribes;
using UnityEngine;

namespace Code.Character.Enemies
{
    public class EnemyCollisionAttack : EnemyCollisionAttackBase, IEventsSubscriber
    {
        [SerializeField] private CollisionObserver _collisionObserver;
        private CollisionAttackData _data;
        private IHero _hero;
        private float _collisionDamage;

        public void Init(IHero hero, CollisionAttackData data,float collisionDamage)
        {
            _data = data;
            _hero = hero;
            _collisionDamage = collisionDamage; 
            SubscribeToEvents(true);
        }

        private void OnDisable()
        {
            SubscribeToEvents(false);
        }

        public void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _collisionObserver.OnEnter += OnEnter;
            }
            else
            {
                _collisionObserver.OnEnter -= OnEnter;
            }
        }

        private void OnEnter(Collision collision)
        {
            var pushForward = Vector3.Reflect(_hero.Rigidbody.velocity.normalized, collision.GetContact(0).normal);
            _hero.EffectsController.Push(pushForward * _data.PushForce);
            _hero.Health.TakeDamage(_collisionDamage * _data.DamageMultiplayer);
        }


        protected override void StartCollisionAttack()
        {
        }
    }
}