using Code.Services;
using UnityEngine;

namespace Code.UI.Actors
{
    public class HudAdapter: MonoBehaviour
    {
        private readonly MovementLimiter _movementLimiter;

        public HudAdapter(Hud hud, MovementLimiter movementLimiter)
        {
            _movementLimiter = movementLimiter;
            
            hud.OnUIWindowShown += OnUIWindowShown;
            hud.OnUIWindowHidden += OnUIWindowHidden;
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