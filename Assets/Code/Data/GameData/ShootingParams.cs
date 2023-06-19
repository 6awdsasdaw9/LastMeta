using System;
using Code.Data.Configs;
using Code.Data.GameData;
using Code.Logic.Missile;
using UnityEngine;

namespace Code.Character.Hero
{
    [Serializable]
    public class ShootingParams : AbilitySettings
    {
        public MissileType MissileType;
        public MissileMoveType MoveType;
        public bool IsMassAttack;

        [Space] [MinMaxRange(0.5f, 5)] public RangedFloat LifeTime;
        [MinMaxRange(0.5f, 5)] public RangedFloat Speed;
        [MinMaxRange(-5, 5)] public RangedFloat Scatter;
        public DamageParam DamageParam;
        public float AttackCooldown;
        public float AbilityCooldown;

        [Space] public Sprite[] MissileSprites;
    }
}