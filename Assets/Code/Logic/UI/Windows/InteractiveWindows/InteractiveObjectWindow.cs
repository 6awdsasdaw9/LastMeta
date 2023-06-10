using System;
using Code.UI.HeadUpDisplay;
using Code.UI.Windows.Animation;
using UnityEngine;

namespace Code.UI.Windows
{
    public class InteractiveObjectWindow : MonoBehaviour, IWindow
    {
        [SerializeField] protected Hud _hud;
        [SerializeField] protected  WindowAnimation _animation;
        
        public virtual void ShowWindow(Action WindowShowed)
        {
            if(_animation.IsPlay)
                return;
            
            _animation.PlayShow(WindowShowed);
            _hud.OnUIWindowShown?.Invoke();
        }

        public virtual void HideWindow(Action WindowHidden)
        {
            if(_animation.IsPlay)
                return;
            
            _animation.PlayHide(WindowHidden);
            _hud.OnUIWindowHidden?.Invoke();
        }
    }
}