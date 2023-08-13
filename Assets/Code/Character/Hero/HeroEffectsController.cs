using System.Threading;
using Code.Character.Hero.HeroInterfaces;
using Code.Services;
using Code.Services.EventsSubscribes;
using Code.Services.Input;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroEffectsController : MonoBehaviour, IEventsSubscriber, IHeroEffectsController
    {
        private Cooldown _disableMovementCooldown;
        private IHero _hero;
        private InputService _inputService;
        private bool _isPressMove;
        private CancellationTokenSource _cts;
        private bool _isPushed;

        [Inject]
        private void Construct(DiContainer container)
        {
            _hero = GetComponent<IHero>();
            _inputService = container.Resolve<InputService>();

            _disableMovementCooldown = new Cooldown();
            _disableMovementCooldown.SetMaxTime(0.15f);
        }

        public void Push(Vector3 forward)
        {
            StopMoveAndPush(forward).Forget();
        }

        private async UniTaskVoid StopMoveAndPush(Vector3 forward)
        {
            if (_isPushed) return;
            _isPushed = true;
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            _disableMovementCooldown.SetMaxCooldown();

            SubscribeToEvents(true);
            _hero.Movement.DisableComponent();
            _hero.Jump.DisableComponent();

            _hero.Rigidbody.AddForce(forward, ForceMode.Impulse);
            await UniTask.WaitUntil(() => _disableMovementCooldown.IsUp() || _isPressMove,
                cancellationToken: _cts.Token);

            SubscribeToEvents(false);
            _hero.Movement.EnableComponent();
            _hero.Jump.EnableComponent();
            _isPushed = false;
        }

        public void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _inputService.OnPressMovement += OnPressMovement;
            }
            else
            {
                _inputService.OnPressMovement -= OnPressMovement;
            }
        }

        private void OnPressMovement(InputAction.CallbackContext context)
        {
            _isPressMove = context.started && _disableMovementCooldown.Normalize < 0.3f;
        }
    }

    public enum Forward
    {
        Up,
        UoRight,
        Right,
        DownRigh,
        Down,
        DownLeft,
        Left,
        UpLeft
    }
}