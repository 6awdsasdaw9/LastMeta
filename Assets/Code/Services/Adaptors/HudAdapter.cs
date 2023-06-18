using System.Linq;
using Code.Logic.Interactive;
using Code.PresentationModel.HeadUpDisplay;
using Code.PresentationModel.Windows;
using Code.Services;
using Code.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.Logic.Adaptors
{
    public class HudAdapter : MonoBehaviour
    {
        private Hud _hud;

        private MovementLimiter _movementLimiter;
        private InputService _input;
        
        [Inject]
        public void Construct(Hud hud,MovementLimiter movementLimiter, InputService inputService)
        {
            _hud = hud;
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

                SubscribeDialogueWindowEvents(true);
            }
            else
            {
                _hud.OnUIWindowShown -= DisableMovement;
                _hud.OnUIWindowHidden -= EnableMovement;

                SubscribeDialogueWindowEvents(true);
            }
        }

        private void SubscribeDialogueWindowEvents(bool flag)
        {
            var dialogueWindow = (IDialogueWindow)_hud.InteractiveObjectWindows
                .FirstOrDefault(w => w.Type == InteractiveObjectType.Dialogue)
                ?.InteractiveObjectWindow;

            if (dialogueWindow == null)
                return;
            
            if (flag)
            {
                dialogueWindow.CloseDefaultButton.OnStartTap += SimulatePressingEsc;
            }
            else
            {
                dialogueWindow.CloseDefaultButton.OnStartTap -= SimulatePressingEsc;
            }
        }

        private void SimulatePressingEsc() => 
            _input.SimulatePressEsc();

        private void EnableMovement() => 
            _movementLimiter.EnableMovement();

        private void DisableMovement() => 
            _movementLimiter.DisableMovement();
    }
}