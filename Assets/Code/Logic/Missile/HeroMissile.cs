using System;
using System.Threading;
using Code.Character.Hero;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.GameData;
using Code.Logic.Missile.MissilesAttack;
using Code.Logic.Missile.MissilesMove;
using Code.Services;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Logic.Missile
{
    public enum MissileType
    {
        Bubble,
        Rocket
    }
    
    public enum MissileMoveType
    {
        Levitation,
        Direct,
        Random,
        Ricochet
    }
    public class HeroMissile : MonoBehaviour
    {
        public IHero Hero;
        public ShootingParams ShootingParams { get; private set; }
        public Rigidbody Rigidbody => _rigidbody;
        [SerializeField] private Rigidbody _rigidbody;
        public MissileModel Model => _model;
        [SerializeField] private MissileModel _model;

        public HeroMissileAttack Attack => _attack;
        [SerializeField] private  HeroMissileAttack _attack;
       
        [SerializeField] private  Collider _collider;
        [ShowInInspector,ReadOnly] public MissileMovement Movement { get; private set; }

        private Cooldown _lifetimeCooldown;
        private CancellationTokenSource _cts;
        public Action<HeroMissile> OnLifetimeCooldownIsUp;
        public Action<HeroMissile> OnTakeDamage;

        public void SetMovement(MissileMovement movement)
        {
            Movement = movement;
        }
        private async  UniTaskVoid StartUpdateLifeTimeCooldown()
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            _lifetimeCooldown = new Cooldown();
            _lifetimeCooldown.SetTime(ShootingParams.LifeTime.GetRandom());
            _lifetimeCooldown.ResetCooldown();

            await UniTask.WaitUntil(_lifetimeCooldown.UpdateCooldown, cancellationToken: _cts.Token);
            OnLifetimeCooldownIsUp?.Invoke(this);
        }

        public void Disable()
        {
            _cts?.Cancel();
            Movement.StopMove();
        }
        
        public class Pool : MonoMemoryPool<ShootingParams, IHero, HeroMissile>
        {
            protected override void OnCreated(HeroMissile item)
            {
                base.OnCreated(item);
            }

            protected override void Reinitialize(ShootingParams data, IHero hero, HeroMissile item)
            {
                item.ShootingParams = data;
                item.Hero = hero;
                item.transform.position = data.StartPoint(hero.Transform);
                item.Rigidbody.useGravity = data.IsUseGravity;
                item.Rigidbody.mass = data.Mass;
                item.Rigidbody.drag = data.Drag;
                item._collider.material = data.PhysicMaterial;
                
                item.Model.SetRandomMissileSprite();
                item.StartUpdateLifeTimeCooldown().Forget();
                base.Reinitialize(data,  hero, item);
            }

            protected override void OnDeSpawned(HeroMissile item)
            {
                item.Rigidbody.velocity = Vector3.zero;
                base.OnDeSpawned(item);
            }
        }
    }


    
    
}