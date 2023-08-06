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
        private IHero _hero;
        private InputService _inputService;
        [SerializeField] private Cooldown _pushDuration;
        private bool _isPressMove;
        private CancellationTokenSource _cts;

        [Inject]
        private void Construct(DiContainer container)
        {
            _hero = GetComponent<IHero>();
            _inputService = container.Resolve<InputService>();
        }
        public void Push(Vector3 forward)
        {
            StopMoveAndPush(forward).Forget();
        }

        private async UniTaskVoid StopMoveAndPush(Vector3 forward)
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            SubscribeToEvents(true);
            _hero.Movement.DisableComponent();
            _hero.Jump.DisableComponent();
            _hero.Rigidbody.AddForce(forward, ForceMode.Impulse);
            await UniTask.WaitUntil(() => _pushDuration.IsUp() || _isPressMove, cancellationToken: _cts.Token);
            _hero.Movement.EnableComponent();
            _hero.Jump.EnableComponent();
            SubscribeToEvents(false);
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
            if (context.started && _pushDuration.Normalize < 0.3f) _isPressMove = true;
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