using UnityEngine;

namespace Code.UI.Windows
{
    [RequireComponent(typeof(InteractiveWindowAnimation))]
    public class InteractiveObjectWindow : MonoBehaviour, IWindow
    {
        [SerializeField] private HUD _hud;
        [SerializeField] private InteractiveWindowAnimation _animation;
        
        public void ShowWindow()
        {
            if(_animation.isMove)
                return;

            _animation.PlayShow();
            _hud.OnUIWindowShown?.Invoke();
        }

        public void HideWindow()
        {
            if(_animation.isMove)
                return;
            
            _animation.PlayHide();
            _hud.OnUIWindowHidden?.Invoke();
        }
    }
}