using Code.Character.Common.CommonCharacterInterfaces;
using UnityEngine;

namespace Code.Logic.Missile
{
    public class MissileAttack: MonoBehaviour
    {
        [SerializeField] private Missile Missile;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IHealth health))
            {
                health.TakeDamage(Missile.ShootingParams.DamageParam.damage);
            }
            
            Missile.OnTakeDamage?.Invoke(Missile);
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