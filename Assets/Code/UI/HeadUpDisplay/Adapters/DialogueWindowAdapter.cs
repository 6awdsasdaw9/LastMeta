using System.Linq;
using Code.Logic.Objects.Interactive;
using Code.Services;
using Code.Services.EventsSubscribes;
using Code.Services.Input;
using Code.UI.HeadUpDisplay.Windows;

namespace Code.UI.HeadUpDisplay.Adapters
{
    public class DialogueWindowAdapter : IEventsSubscriber
    {
        private readonly HudFacade _hudFacade;
        private readonly InputService _inputService;

        public DialogueWindowAdapter(HudFacade hudFacade, InputService inputService, EventSubsribersStorage eventSubsribersStorage)
        {
            _hudFacade = hudFacade;
            _inputService = inputService;
            eventSubsribersStorage.Add(this);
            SubscribeToEvents(true);
        }
        
        public void SubscribeToEvents(bool flag)
        {
            var dialogueWindow = (IDialogueWindow)_hudFacade.InteractiveObjectWindows
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
            _inputService.SimulatePressEsc();
        }
    }
}