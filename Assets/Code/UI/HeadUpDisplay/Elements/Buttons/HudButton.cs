using System;
using Code.Audio.AudioEvents;
using Code.Data.Configs;
using FMODUnity;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.HeadUpDisplay.Elements.Buttons
{
    public  class HudButton: HudElement
    {
        [SerializeField] private Button _button;
        private readonly AudioEvent _audioEvent = new AudioEvent();
        private EventReference _buttonAudioEvent;
        public event Action OnStartTap;

        [Inject]
        private void Construct(DiContainer container)
        {
            _buttonAudioEvent = container.Resolve<HudSettings>().ButtonAudioEvent;
        }
        
        protected void OnEnable()
        {
            _button.onClick.AddListener(InvokeEvent);
        }
        protected  void OnDisable()
        {
            _button.onClick.RemoveListener(InvokeEvent);
        }

        protected virtual void InvokeEvent()
        {
            OnStartTap?.Invoke();
            _audioEvent.PlayAudioEvent(_buttonAudioEvent);
        }
    }
}