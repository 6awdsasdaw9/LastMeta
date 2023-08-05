using System;
using Code.Character.Hero.HeroInterfaces;
using Cysharp.Threading.Tasks;
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
        
        public async UniTaskVoid Push()
        {
            if (_pushData.Force == 0) return;
            _hero.Movement.SetSupportVelocity((_owner.position - _hero.Transform.position) * _pushData.Force);
            await UniTask.Delay(TimeSpan.FromSeconds(_pushData.Duration != 0 ? _pushData.Duration : 0.5f));
            _hero.Movement.SetSupportVelocity(Vector2.zero);
        }
    }

    [Serializable]
    public struct PushData
    {
        public float Duration;
        public float Force;
    }
}