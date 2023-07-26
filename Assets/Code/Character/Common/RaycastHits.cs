using System.Collections.Generic;
using Code.Character.Common.CommonCharacterInterfaces;
using Code.Debugers;
using UnityEngine;

namespace Code.Character.Common
{
    public class RaycastHits
    {
        private readonly Transform _owner;
        private readonly Collider[] _hits;
        private readonly int _layerMask;
        private readonly float _hitOffsetX;
        private readonly float _hitRadius;
        
        public RaycastHits(Transform owner,string layerName, float hitRadius =1, int hitsSize =1, float hitOffsetX = 0.2f)
        {
            _owner = owner;
            _layerMask =  LayerMask.GetMask(layerName);
            _hitRadius = hitRadius;
            _hits = new Collider[hitsSize];
            _hitOffsetX = hitOffsetX;
        }

        private int Hit() =>
            Physics.OverlapSphereNonAlloc(StartPoint(), _hitRadius, _hits, _layerMask);
        private Vector3 StartPoint() =>
            new(_owner.position.x + _owner.localScale.x * _hitOffsetX, _owner.position.y + 0.7f, _owner.position.z);

        public List<T> GetComponents<T>() where T : class
        {
            PhysicsDebug.DrawDebug(StartPoint(), _hitRadius, 1.0f);
            List<T> collection = new List<T>();
            for (var i = 0; i < Hit(); i++)
            {
                _hits[i].TryGetComponent(out T value);
             
                if (value == null)
                    continue;
                
                collection.Add(value);
            }

            return collection;
        }
    }
}