using Code.Character.Common.CommonCharacterInterfaces;
using UnityEngine;

namespace Code.Logic.Missile
{
    public class HeroMissileAttack: MonoBehaviour
    {
        [SerializeField] private HeroMissile _heroMissile;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IHealth health))
            {
                health.TakeDamage(_heroMissile.Hero.Stats.Damage);
            }
            
            _heroMissile.OnTakeDamage?.Invoke(_heroMissile);
        }
    }

    public class MissileMassAttack
    {
        private readonly Collider[] _hits = new Collider[7];
        private int _layerMask;

        public MissileMassAttack()
        {
            _layerMask = 1 << LayerMask.NameToLayer(Constants.HittableLayer);
        }
    }
}