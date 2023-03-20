using Code.Character.Interfaces;
using Code.Data.GameData;
using Code.Data.States;
using Code.Debugers;
using Code.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroAttack : MonoBehaviour
    {
        [SerializeField] private HeroAnimator _animator;
        private InputService _inputService;
        private PowerData _power;
        private Collider[] _hits = new Collider[7];
        public bool attackIsActive { get; private set; }
        private int _layerMask;

        [Inject]
        private void Construct(InputService inputService, ConfigData configData)
        {
            _inputService = inputService;
            _inputService.PlayerAttackEvent += Attack;
            _power = configData.heroConfig.power;
            _layerMask = 1 << LayerMask.NameToLayer(Constants.HittableLayer);
        }

        private void OnDestroy()
        {
            _inputService.PlayerAttackEvent -= Attack;
        }

        private void Attack()
        {
            if (attackIsActive)
                return;

            _animator.PlayAttack();
            attackIsActive = true;
        }

        /// <summary>
        /// Animation Event
        /// </summary>
        private void OnAttack()
        {
            Log.ColorLog("On Attack");
            PhysicsDebug.DrawDebug(StartPoint(), _power.damagedRadius, 1.0f);
            for (int i = 0; i < Hit(); ++i)
            {
                    Log.ColorLog("Hit");
             _hits[i].GetComponent<IHealth>().TakeDamage(_power.damage);
                /*if (_hits[i].TryGetComponent(out IHealth health))
                {
                    Log.ColorLog("On Hit");
                    health.TakeDamage(_power.damage);
                }*/
            }
        }

        /// <summary>
        /// Animation Event
        /// </summary>
        private void OnAttackEnded() => attackIsActive = false;

        private int Hit() =>
            Physics.OverlapSphereNonAlloc(StartPoint() , _power.damagedRadius, _hits, _layerMask);

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x + transform.localScale.x * 0.1f, transform.position.y + 0.7f,
                transform.position.z);
    }
}