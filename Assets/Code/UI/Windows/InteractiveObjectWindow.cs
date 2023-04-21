using UnityEngine;

namespace Code.UI.Windows
{
    [RequireComponent(typeof(VerticalWindowAnimation))]
    public class InteractiveObjectWindow : MonoBehaviour, IWindow
    {
        [SerializeField] private HUD _hud;
        [SerializeField] private WindowAnimation _animation;
        
        public virtual void ShowWindow()
        {
            if(_animation.IsPlay)
                return;

            _animation.PlayShow();
            _hud.OnUIWindowShown?.Invoke();
        }

        public virtual  void HideWindow()
        {
            if(_animation.IsPlay)
                return;
            
            _animation.PlayHide();
            _hud.OnUIWindowHidden?.Invoke();
        }
    }
}