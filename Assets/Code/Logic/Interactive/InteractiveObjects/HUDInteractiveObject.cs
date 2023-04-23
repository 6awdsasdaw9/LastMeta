using System.Linq;
using Code.UI;
using Code.UI.Windows;
using Zenject;

namespace Code.Logic.Interactive.InteractiveObjects
{
    public class HUDInteractiveObject : Interactivity
    {
        private IWindow _presentationWindow;
        private bool _isWindowNull;
        
        [Inject]
        private void Construct(HUD hud)
        {
            hud.InteractiveObjectWindows
                .FirstOrDefault(w => w.Type == Type)?
                .InteractiveObjectWindow
                .TryGetComponent(out _presentationWindow);
           
            _isWindowNull = _presentationWindow == null;
        }
        
        public override void StartInteractive()
        {
            if(_isWindowNull)
                return;

            OnProcess = true;
            OnStartInteractive?.Invoke();
            _presentationWindow.ShowWindow(() => OnProcess = false);
        }

        public override void StopInteractive()
        {
            if(_isWindowNull)
                return;
            
            OnProcess = true;
            OnEndInteractive?.Invoke();
            _presentationWindow.HideWindow(() => OnProcess = false);
        }
        
        
    }
}