using System.Collections.Generic;
using System.Linq;
using Code.Character.Hero.HeroInterfaces;
using Code.Infrastructure.GlobalEvents;
using Code.Logic.Objects.Interactive;
using Code.PresentationModel.HeadUpDisplay;
using Code.PresentationModel.Windows;
using Code.Services;
using Code.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.Logic.Adaptors
{
    public class HudWindowsAdapter : MonoBehaviour
    {
        private Hud _hud;
        private MovementLimiter _movementLimiter;
        private InputService _input;
        
        private MenuWindowAdapter _menuWindowAdapter;
        private HeroInformationWindowAdapter _heroInformationWindowAdapter;
        
        private List<IWindow> _openedWindows = new();
        private bool _menuWindowIsOpened;
        private EventsFacade _eventsFacade;

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
            _eventsFacade = eventsFacade;
        
            
            InitWindowsAdapters(hud, eventsFacade, hero);
        }

        private void OnEnable() =>
            SubscribeToEvents(true);

        private void OnDisable() =>
            SubscribeToEvents(false);

        private void InitWindowsAdapters(Hud hud, EventsFacade eventsFacade, IHero hero)
        {
            _menuWindowAdapter = new MenuWindowAdapter(eventsFacade, hud, _input);
        
            if (_hud.GameMode == Constants.GameMode.Real) return;

            _heroInformationWindowAdapter = new HeroInformationWindowAdapter(eventsFacade, hud, hero);
        }

        private void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _eventsFacade.HudEvents.OnWindowShown += AddOpenedWindowToList;
                _eventsFacade.HudEvents.OnWindowHidden += RemoveOpenedWindowFromList;
            }
            else
            {
                _eventsFacade.HudEvents.OnWindowShown -= AddOpenedWindowToList;
                _eventsFacade.HudEvents.OnWindowHidden -= RemoveOpenedWindowFromList;
            }

            SubscribeDialogueWindowEvents(flag);
            _menuWindowAdapter.SubscribeToEvent(flag);
            _heroInformationWindowAdapter.SubscribeToEvent(flag);
        }

        private void RemoveOpenedWindowFromList(IWindow window)
        {
            _openedWindows.Remove(window);
            if(_openedWindows.Any())return;
            _eventsFacade.HudEvents.CloseLastWindowEvent();
        }

        private void AddOpenedWindowToList(IWindow window)
        {
            _openedWindows.Add(window);
            if(_openedWindows.Count != 1)return;
            _eventsFacade.HudEvents.OpenFirstWindowEvent();
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

        private void SimulatePressingEsc()
        {
            _input.SimulatePressEsc();
        }
        
    }
}