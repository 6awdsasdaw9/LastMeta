using System.Linq;
using Code.UI;
using Code.UI.Windows;
using UnityEngine;
using Zenject;

namespace Code.Logic.Interactive.InteractiveObjects.Laptop
{
    public class InteractiveObject : MonoBehaviour,IInteractive
    {
        [SerializeField] private InteractiveObjectType _type;
        private InteractiveObjectWindow _window;
        private bool _isWindowNull;
        
        [Inject]
        private void Construct(HUD hud)
        {
            _window = hud.InteractiveObjectWindows.FirstOrDefault(w => w.Type == _type)?.InteractiveObjectWindow;
            _isWindowNull = _window == null;
        }
        public void StartInteractive()
        {
            if(_isWindowNull)
                return;
            _window.ShowWindow();
        }

        public void StopInteractive()
        {
            if(_isWindowNull)
                return;
            _window.HideWindow();
        }
        
        
    }
}