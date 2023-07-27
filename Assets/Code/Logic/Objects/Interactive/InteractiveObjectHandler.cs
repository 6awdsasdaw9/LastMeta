using Code.Audio.AudioEvents;
using Code.Data.Configs;
using Code.Logic.Objects.Animations;
using Code.Logic.Objects.Interactive.InteractiveObjects;
using Code.Logic.Triggers;
using Code.Services;
using Code.Services.Input;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Logic.Objects.Interactive
{
    public class InteractiveObjectHandler : FollowTriggerObserver, IEventsSubscriber
    {
        [SerializeField] private InteractiveIconType iconType = InteractiveIconType.Interaction;
        [SerializeField] private InteractiveIconAnimation _iconAnimation;

        [Space, Title("Optional")] 
        [SerializeField] private bool _isStartOnEnable;
        [SerializeField] private AudioEvent _pressButtonAudioEvent;
        
        private Interactivity _interactiveObject;
        private InputService _input;
        private Cooldown _cooldown;

        private bool _onInteractive;
        private float _currentCooldown;

        [Inject]
        private void Construct(DiContainer container)
        {
            _input = container.Resolve<InputService>();
            container.Resolve<EventSubsribersStorage>().Add(this);
            var hudSettings = container.Resolve<HudSettings>();

            _cooldown.SetTime(hudSettings.InteractiveUIParams.InteractiveCooldownTime);
            _cooldown = new Cooldown();
            
            _interactiveObject = GetComponent<Interactivity>();
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
                ShowIcon().Forget();
            }
        }

        private void Update()
        {
            if (_interactiveObject == null) return;

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
        
        public void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _interactiveObject.OnEndInteractive += StopInteractive;
            }
            else
            {
                _interactiveObject.OnEndInteractive -= StopInteractive;
            }
        }
        
        private bool IsReady() => _cooldown.IsUp() && !_interactiveObject.OnAnimationProcess;

        private void StartInteractive()
        {
            _onInteractive = true;
            _interactiveObject.StartInteractive();
            _pressButtonAudioEvent.PlayAudioEvent();
         
            SubscribeToEvents(_onInteractive);
            HideIcon();
        }

        private void StopInteractive()
        {
            _onInteractive = false;
            _interactiveObject.StopInteractive();
            _pressButtonAudioEvent.PlayAudioEvent();
            
            SubscribeToEvents(_onInteractive);
            ShowIcon().Forget();
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
            await UniTask.WaitUntil(
                () => _cooldown.IsUp(),
                cancellationToken: gameObject.GetCancellationTokenOnDestroy());

            _iconAnimation.PlayType(iconType);
        }

        private void HideIcon() =>
            _iconAnimation.PlayVoid();


    }
}