using Code.Character.Hero.HeroInterfaces;
using Code.Data.GameData;
using Code.Logic.Common;
using Code.Logic.Common.Interfaces;
using Code.Services.EventsSubscribes;
using Code.Services.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroAttack : MonoBehaviour, IHeroAttack, IEventsSubscriber
    {
        [SerializeField] private Hero _hero;
        private InputService _inputService;
        private RaycastHitsController _raycastHitController;
        public AttackData AttackData { get; private set; }
        public bool IsAttack { get; private set; }

        private int _clickCount;

        private bool _isCanAttack => !_hero.Stats.IsDash
                                     && _hero.Stats.OnGround
                                     && !_hero.Stats.IsCrouch
                                     && !_hero.Stats.IsBlockMove;

        [Inject]
        private void Construct(InputService inputService)
        {
            _inputService = inputService;
        }

        private void OnEnable()
        {
            SubscribeToEvents(true);
        }

        private void OnDisable()
        {
            SubscribeToEvents(false);
        }

        public void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _inputService.OnPressAttackButton += StartAttack;
                _inputService.OnPressMovement += InputServiceOnOnPressMovement;
            }
            else
            {
                _inputService.OnPressAttackButton -= StartAttack;
                _inputService.OnPressMovement -= InputServiceOnOnPressMovement;
            }
        }

        private void InputServiceOnOnPressMovement(InputAction.CallbackContext obj)
        {
            if(IsAttack) StopAttack();
        }

        private void StopAttack()
        {
            _hero.Animator.PlayStopAttack();
        }

        public void EnableComponent()
        {
            if (this != null) enabled = true;
        }


        public void DisableComponent()
        {
            if (this != null) enabled = false;
        }

        public void SetDamageParam(AttackData attackData)
        {
            AttackData = attackData;

            _raycastHitController = new RaycastHitsController(
                owner: _hero.Transform,
                layerName: Constants.HittableLayer,
                hitRadius: 0.2f,
                hitsSize: 7,
                hitOffsetX: attackData.EffectiveDistance.x,
                hitOffsetY: attackData.EffectiveDistance.y);

            _hero.Animator.SetMelleAttackSpeed(attackData.AnimationSpeed);
        }

        private void StartAttack()
        {
            AndComboCount();

            if (IsAttack || !_isCanAttack)
                return;

            IsAttack = true;
            _hero.Animator.PlayAttack();
            _hero.Movement.BlockMovement();
        }

        private void AndComboCount()
        {
            if (_clickCount >= AttackData.MaxCombo)
            {
                return;
            }

            _clickCount++;
            _hero.Animator.SetMaxCombo(_clickCount);
        }

        /// <summary>
        /// Animation Event
        /// </summary>
        public void OnAttack()
        {
            var damageTakers = _raycastHitController.GetComponents<IHealth>();

            foreach (var health in damageTakers)
            {
                health.TakeDamage(AttackData.Damage);
            }
        }

        /// <summary>
        /// Animation Event
        /// </summary>
        public void OnAttackEnded()
        {
            IsAttack = false;
            _clickCount = 0;
            _hero.Movement.UnBlockMovement();
        }
    }
}