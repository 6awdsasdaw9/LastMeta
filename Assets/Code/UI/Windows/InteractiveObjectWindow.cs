using System;
using UnityEngine;

namespace Code.UI.Windows
{
    public class InteractiveObjectWindow : MonoBehaviour, IWindow
    {
        [SerializeField] private HUD _hud;
        [SerializeField] private WindowAnimation _animation;
        
        public virtual void ShowWindow(Action WindowShowed)
        {
            if(_animation.IsPlay)
                return;
            
            _animation.PlayShow(WindowShowed);
            _hud.OnUIWindowShown?.Invoke();
        }

        public virtual  void HideWindow(Action WindowHidden)
        {
            if(_animation.IsPlay)
                return;
            
            _animation.PlayHide(WindowHidden);
            _hud.OnUIWindowHidden?.Invoke();
        }
    }
}