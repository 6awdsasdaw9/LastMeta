using Code.Data.Configs;
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
            _input.OnPressEsc += () =>
            {
                if (IsReady() && _onInteractive)
                {
                    PlayAudioEvent();
                    StopInteractive();
                }
            };

            _cooldown = new Cooldown();
            _cooldown.SetTime(gameSettings.InteractiveCooldownTime);

            _interactiveObject = GetComponent<IInteractive>();
        }

        private void OnEnable()
        {
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

                PlayAudioEvent();
                _cooldown.ResetCooldown();
            }

            _cooldown.UpdateCooldown();
        }

        private void OnDisable()
        {
            HideIcon();

            _input.OnPressEsc -= () =>
            {
                if (IsReady() && _onInteractive)
                {
                    PlayAudioEvent();
                    StopInteractive();
                }
            };
        }

        private bool IsReady() => _cooldown.IsUp() && !_interactiveObject.OnProcess;
        
        private void StopInteractive()
        {
            _onInteractive = false;
            _interactiveObject.StopInteractive();
            ShowIcon();
        }

        private void StartInteractive()
        {
            _interactiveObject.StartInteractive();
            _onInteractive = true;
            HideIcon();
        }

        private async void ShowIcon()
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