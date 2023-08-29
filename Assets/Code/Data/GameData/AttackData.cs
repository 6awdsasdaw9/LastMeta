using System;
using UnityEngine;

namespace Code.Data.GameData
{
    [Serializable]
    public class AttackData
    {
        public float Damage;
        public float DamagedRadius;
        public Vector3 EffectiveDistance = new(0.5f,1,0);
        [Range(0, 3)]public float PushForce = 1;
        [Range(0, 15)]public float Cooldown = 0.7f;
        [Range(1,2.5f)]public float AnimationSpeed = 1;
        [Range(1, 3)] public int MaxCombo = 1;
    }

    [Serializable]
    public class SpikeAttackData
    {
        [Range(0, 15)]public float Cooldown = 0.7f;
        [Range(1,2.5f)]public float AnimationSpeed = 1;
    }

    [Serializable]
    public class CollisionAttackData
    {
        public float DamageMultiplayer = 1;
        [Range(0, 3)]public float PushForce = 1;
        [Range(0, 15)]public float Cooldown = 0.7f;
    }
}