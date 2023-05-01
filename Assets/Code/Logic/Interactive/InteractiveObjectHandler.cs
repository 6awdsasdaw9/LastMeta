using Code.Data.Configs;
using Code.Debugers;
using Code.Logic.Interactive.InteractiveObjects;
using Code.Logic.Triggers;
using Code.Services;
using Code.Services.Input;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Logic.Interactive
{
    public class InteractiveObjectHandler : FollowTriggerObserver
    {
        [SerializeField] private InteractiveIconType iconType = InteractiveIconType.Interaction;
        [SerializeField] private InteractiveIconAnimation _iconAnimation;
        [SerializeField] private AudioEvent _audioEvent;

        [SerializeField] private bool _isStartOnEnable;

        private IInteractive _interactiveObject;
        private InputService _input;
        private Cooldown _cooldown;

        private bool _onInteractive;
        private float _currentCooldown;

        [Inject]
        private void Construct(InputService inputService, GameSettings gameSettings)
        {
            _input = inputService;

            _cooldown = new Cooldown();
            _cooldown.SetTime(gameSettings.InteractiveCooldownTime);

            _interactiveObject = GetComponent<IInteractive>();
        }

        private void OnEnable()
        {
            _input.OnPressEsc += OnPressEsc;
            if (_isStartOnEnable)
            {
                StartInteractive();
            }
            else
            {
                ShowIcon();
            }
        }

        private void Update()
        {
            if (_interactiveObject == null)
                return;

            if (_input.GetInteractPressed() && IsReady())
            {
                if (_onInteractive)
                {
                    StopInteractive();
                }
                else
                {
                    StartInteractive();
                }

                _cooldown.ResetCooldown();
            }

            _cooldown.UpdateCooldown();
        }

        private void OnDisable()
        {
            _input.OnPressEsc -= OnPressEsc;
            HideIcon();
        }

        private bool IsReady() => _cooldown.IsUp() && !_interactiveObject.OnProcess;

        private void StartInteractive()
        {
            _interactiveObject.StartInteractive();
            _onInteractive = true;
            HideIcon();
            PlayAudioEvent();
        }

        private void StopInteractive()
        {
            _onInteractive = false;
            _interactiveObject.StopInteractive();
            ShowIcon().Forget();
            PlayAudioEvent();
        }

        private void OnPressEsc()
        {
            if (IsReady() && _onInteractive)
            {
                StopInteractive();
            }
        }

        private async UniTaskVoid ShowIcon()
        {
            await UniTask.WaitUntil(() => _cooldown.IsUp());
            _iconAnimation?.PlayType(iconType);
        }

        private void HideIcon() =>
            _iconAnimation?.PlayVoid();

        private void PlayAudioEvent()
        {
            if (_audioEvent != null)
            {
                _audioEvent?.PlayAudioEvent();
            }
        }
    }
}