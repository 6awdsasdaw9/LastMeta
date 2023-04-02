using Code.Logic.Triggers;
using Code.Services;
using Code.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.Logic.Interactive
{
    [RequireComponent(typeof(TriggerObserverAdapter))]
    public class InteractiveObjectHandler : FollowTriggerObserver
    {
        [SerializeField] private InteractiveType _type;
        [SerializeField] private InteractiveIconAnimation _iconAnimation;
        [SerializeField] private Cooldown _cooldown;
        
        private IInteractive _interactiveObject;
        private InputService _input;
        private bool _onInteractive;
        private float _currentCooldown;
        
        [Inject]
        private void Construct(InputService inputService)
        {
            _input = inputService;
            _interactiveObject = GetComponent<IInteractive>();
        }

        private void OnEnable()
        {
            ShowIcon();
        }

        private void Update()
        {
            if(_interactiveObject == null)
                return;
            
            if (_input.GetInteractPressed() && _cooldown.IsUp())
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
            HideIcon();
        }

        private void StopInteractive()
        {
            _interactiveObject.StopInteractive();
            _onInteractive = false;
            ShowIcon();
        }

        private void StartInteractive()
        {
            _interactiveObject.StartInteractive();
            _onInteractive = true;
            HideIcon();
        }

        private void ShowIcon()
        {
            _iconAnimation.PlayType(_type);
        }

        private void HideIcon()
        {
            _iconAnimation.PlayVoid();
        }
    }
}