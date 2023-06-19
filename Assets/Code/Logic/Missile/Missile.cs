using System;
using System.Threading;
using Code.Character.Hero;
using Code.Services;
using Cysharp.Threading.Tasks;
using Unity.Collections;
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
    public class Missile : MonoBehaviour
    {
        public ShootingParams ShootingParams { get; private set; }
        public Rigidbody Rigidbody;

        public MissileModel Model;
        [ReadOnly] public MissileAttack Attack;
        [ReadOnly] public MissileMovement Movement;

        private Cooldown _lifetimeCooldown;
        private CancellationTokenSource _cts;
        public Action<Missile> OnLifetimeCooldownIsUp;
        public Action<Missile> OnTakeDamage;

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
        
        public class Pool : MonoMemoryPool<ShootingParams, Vector3,float, Missile>
        {
            protected override void OnCreated(Missile item)
            {
                base.OnCreated(item);
            }

            protected override void Reinitialize(ShootingParams data, Vector3 spawnPosition,float forward, Missile item)
            {
                item.ShootingParams = data;
                item.transform.position = spawnPosition;
                item.Model.SetRandomMissileSprite();
                item.StartUpdateLifeTimeCooldown().Forget();
                base.Reinitialize(data, spawnPosition,forward, item);
            }

            protected override void OnDespawned(Missile item)
            {
                item.Rigidbody.velocity = Vector3.zero;
                base.OnDespawned(item);
            }
        }
    }


    
    
}