using Code.Logic.Triggers;
using Code.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.Logic.Interactive
{
    [RequireComponent(typeof(TriggerObserverAdapter))]
    public class InteractiveObjectAdapter: FollowTriggerObserver
    {
        [SerializeField] private InteractiveIconAnimation _iconAnimation;
        [SerializeField] private InteractiveType _type;
        [SerializeField] private IInteractive _interactive;
        private InputService _input;

        [Inject]
        private void Construct(InputService inputService)
        {
            _input = inputService;
        }

        private void OnEnable()
        {
            ShowIcon();
        }
        
        private void Update()
        {
            if (_input.GetInteractPressed())
            {
                Debug.Log("=)");
            }
        }

        private void OnDisable()
        {
            HideIcon();
        }

        public void ShowIcon()
        {
            _iconAnimation.PlayType(_type);
        }

        public void HideIcon()
        {
            _iconAnimation.PlayVoid();
        }
        
    }

    public enum InteractiveType {
        Void,
        Exclamation,
        Interaction,
        Question,
        Shop
    }
}