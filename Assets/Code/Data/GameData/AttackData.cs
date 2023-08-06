using System;
using UnityEngine;

namespace Code.Data.GameData
{
    [Serializable]
    public class AttackData
    {
        public float Damage;
        public float DamagedRadius;
        public Vector3 EffectiveDistance = new Vector3(.5f,1,0);
        [Range(0, 15)]public float Cooldown = 0.7f;
    }
}