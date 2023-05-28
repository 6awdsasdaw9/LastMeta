using System.Linq;
using Code.Logic.Interactive;
using Code.Services;
using Code.Services.Input;
using Code.UI.HeadUpDisplay;
using Code.UI.Windows;
using UnityEngine;
using Zenject;

namespace Code.UI.Adaptors
{
    public class HudAdapter : MonoBehaviour
    {
        [SerializeField] private HUD _hud;

        private MovementLimiter _movementLimiter;
        private InputService _input;


        [Inject]
        public void Construct(MovementLimiter movementLimiter, InputService inputService)
        {
            _movementLimiter = movementLimiter;
            _input = inputService;
        }

        private void OnEnable() => 
            SubscribeToEvents(true);

        private void OnDisable() => 
            SubscribeToEvents(false);


        private void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _hud.OnUIWindowShown += DisableMovement;
                _hud.OnUIWindowHidden += EnableMovement;

                SubscribeDialogueWindow(true);
            }
            else
            {
                _hud.OnUIWindowShown -= DisableMovement;
                _hud.OnUIWindowHidden -= EnableMovement;

                SubscribeDialogueWindow(true);
            }
        }

        private void SubscribeDialogueWindow(bool flag)
        {
            var dialogueWindow = (IDialogueWindow)_hud.InteractiveObjectWindows
                .FirstOrDefault(w => w.Type == InteractiveObjectType.Dialogue)
                ?.InteractiveObjectWindow;

            if (dialogueWindow == null)
                return;
            
            if (flag)
            {
                dialogueWindow.CloseButton.OnStartTap += SimulatePressingEsc;
            }
            else
            {
                dialogueWindow.CloseButton.OnStartTap -= SimulatePressingEsc;
            }
        }

        private void SimulatePressingEsc() => 
            _input.PressEsc();

        private void EnableMovement() => 
            _movementLimiter.EnableMovement();

        private void DisableMovement() => 
            _movementLimiter.DisableMovement();
    }
}