using System.Collections.Generic;
using Code.Debugers;
using UnityEngine;

namespace Code.Logic.Common
{
    public class RaycastHitsController
    {
        private readonly Transform _owner;
        private readonly Collider[] _hits;
        private readonly int _layerMask;
        private readonly Vector2 _hitOffset;
        private readonly float _hitRadius;

        public RaycastHitsController(
            Transform owner, 
            string layerName, 
            float hitRadius = 1, int hitsSize = 1,
            float hitOffsetX = 0.2f, float hitOffsetY = 0.7f) {
            _owner = owner;
            _layerMask = LayerMask.GetMask(layerName);
            _hitRadius = hitRadius;
            _hits = new Collider[hitsSize];
            _hitOffset = new Vector2(hitOffsetX, hitOffsetY);
        }

        private int Hit() =>
            Physics.OverlapSphereNonAlloc(StartPoint(), _hitRadius, _hits, _layerMask);

        private Vector3 StartPoint()
        {
            Logg.ColorLog($"Scale: { _owner.localScale.x}");
            return new(_owner.position.x + (_hitOffset.x * _owner.localScale.x), _owner.position.y + _hitOffset.y, 0);
        }

        public List<T> GetComponents<T>() where T : class
        {
            PhysicsDebug.DrawDebug(StartPoint(), _hitRadius, 1.0f);
            var collection = new List<T>();
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