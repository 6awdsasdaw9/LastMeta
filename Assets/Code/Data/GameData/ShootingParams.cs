using System;
using Code.Data.Configs;
using Code.Data.GameData;
using Code.Logic.Missile;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Character.Hero
{
    [Serializable]
    public class ShootingParams : AbilitySettings
    {
        [Title("Attack")]
        public bool IsMassAttack;
        public DamageParam DamageParam;
        public float AttackCooldown;

        [Title("Bullet")]
        public MissileType MissileType;
        public MissileMoveType MoveType;
        [Space] [MinMaxRange(0.5f, 5)] public RangedFloat LifeTime;
        [MinMaxRange(0.05f, 5)] public RangedFloat Speed;
        [MinMaxRange(-5, 5)] public RangedFloat Scatter;

        [Title("Bullet Physics")] 
        public float Mass = 0.6f;
        public bool IsUseGravity;
        public PhysicMaterial PhysicMaterial;
        public float Drag = 0.01f;
        
        public Vector2 SpawnOffset = new(0.4f, 0.7f);
        public Vector3 StartPoint(Transform transform) => 
            new(transform.position.x + transform.localScale.x * SpawnOffset.x, 
                transform.position.y + SpawnOffset.y,
                transform.position.z);
        
        [Space] public Sprite[] MissileSprites;
    }
}