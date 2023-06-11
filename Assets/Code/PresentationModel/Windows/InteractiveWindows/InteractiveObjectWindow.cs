using System;
using Code.PresentationModel.HeadUpDisplay;
using Code.PresentationModel.Windows.WindowsAnimation;
using UnityEngine;

namespace Code.PresentationModel.Windows.InteractiveWindows
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