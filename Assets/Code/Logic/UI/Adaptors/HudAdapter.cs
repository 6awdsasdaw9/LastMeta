using Code.Services;
using Code.UI.HeadUpDisplay;
using UnityEngine;
using Zenject;

namespace Code.UI.Adaptors
{
    public class HudAdapter: MonoBehaviour
    {
        [SerializeField] private HUD _hud;
        private  MovementLimiter _movementLimiter;
        
        
        [Inject]
        public void Construct( MovementLimiter movementLimiter)
        {
            _movementLimiter = movementLimiter;
        }

        private void OnEnable()
        {
            _hud.OnUIWindowShown += OnUIWindowShown;
            _hud.OnUIWindowHidden += OnUIWindowHidden;
        }

        private void OnDisable()
        {
            _hud.OnUIWindowShown -= OnUIWindowShown;
            _hud.OnUIWindowHidden -= OnUIWindowHidden;
        }

        private void OnUIWindowHidden()
        {
            _movementLimiter.EnableMovement();
        }

        private void OnUIWindowShown()
        {
            _movementLimiter.DisableMovement();
        }
    }
}