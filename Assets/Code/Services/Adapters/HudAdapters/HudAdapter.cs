using System.Linq;
using Code.Character.Hero.HeroInterfaces;
using Code.Infrastructure.GlobalEvents;
using Code.Logic.Interactive;
using Code.PresentationModel.Buttons;
using Code.PresentationModel.HeadUpDisplay;
using Code.PresentationModel.HudElements.HudButtonWindows;
using Code.PresentationModel.Windows;
using Code.PresentationModel.Windows.MenuWindow;
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
        private MenuWindowAdapter _menuWindowAdapter;
        private HeroInformationWindowAdapter _heroInformationWindowAdapter;

        [Inject]
        public void Construct(Hud hud, 
            MovementLimiter movementLimiter, 
            InputService inputService,
            EventsFacade eventsFacade,
            IHero hero)
        {
            _hud = hud;
            _movementLimiter = movementLimiter;
            _input = inputService;

            _menuWindowAdapter = new MenuWindowAdapter(eventsFacade, hud);

            if (_hud.GameMode == Constants.GameMode.Real)
                return;
            
            _heroInformationWindowAdapter = new HeroInformationWindowAdapter(eventsFacade, hud, hero);
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
            }
            else
            {
                _hud.OnUIWindowShown -= DisableMovement;
                _hud.OnUIWindowHidden -= EnableMovement;
            }

            SubscribeDialogueWindowEvents(flag);
            _menuWindowAdapter.SubscribeToEvent(flag);
            _heroInformationWindowAdapter.SubscribeToEvent(flag);
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