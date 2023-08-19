using System;
using Code.Character.Hero.HeroInterfaces;
using UnityEngine;

namespace Code.Logic.Common
{
    public class HeroPusher
    {
        private readonly Transform _owner;
        private readonly IHero _hero;
        private readonly PushData _pushData;

        public HeroPusher(Transform owner,IHero hero, PushData  pushData)
        {
            _owner = owner;
            _hero = hero;
            _pushData = pushData;
        }
        
        public void Push()
        {
            var pushDirection = GetDirection();
            _hero.EffectsController.Push(pushDirection * _pushData.Force);
        }

        private Vector3 GetDirection()
        {
            Vector3 impactVector = _hero.Transform.position - _owner.position;
            var rotationAngle = _owner.rotation.eulerAngles.z;
            
            impactVector = Quaternion.Euler(0f, 0f, -rotationAngle) * impactVector;
            return impactVector.normalized; 
        }
    }

    [Serializable]
    public struct PushData
    {
        public float Force;
    }
}