using System;
using Code.Audio.AudioEvents;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.HeadUpDisplay.Elements.Buttons
{
    public  class HudButton: HudElement
    {
        [SerializeField] private Button _button;
        [Title("Optional")]
        [GUIColor(0.85f, 0.74f, 1)]
        [SerializeField] private AudioEvent _audioEvent;
        public event Action OnStartTap;

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
            _audioEvent.PlayAudioEvent();
        }
    }
}