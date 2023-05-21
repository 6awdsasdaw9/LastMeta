using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Buttons
{
    public abstract class ButtonTap: MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        public event Action OnStartTap;

        private void OnEnable()
        {
            _button.onClick.AddListener(InvokeEvent);
        }
        private void OnDisable()
        {
            _button.onClick.RemoveListener(InvokeEvent);
        }

        private void InvokeEvent()
        {
            OnStartTap?.Invoke();
        }
    }
}