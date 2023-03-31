using System;
using Code.Character.Common;
using Code.Services;
using Code.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.Logic
{
    public class InteractiveObject: FollowTriggerObserver
    {
      
        [SerializeField] private InteractiveIconAnimation _iconAnimation;
        [SerializeField] private InteractiveType _type;
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