using System;
using Code.UI.HeadUpDisplay.Windows.HudElementsAnimation;
using UnityEngine;

namespace Code.UI.HeadUpDisplay.Windows.InteractiveWindows
{
    public class InteractiveObjectWindow : MonoBehaviour, IWindow
    {
        [SerializeField] protected HudFacade hudFacade;
        [SerializeField] protected  WindowAnimation _animation;
        
        public virtual void ShowWindow(Action WindowShowed)
        {
            if(_animation.IsPlay)
                return;
            
            _animation.PlayShow(WindowShowed);
        }

        public virtual void HideWindow(Action WindowHidden)
        {
            if(_animation.IsPlay)
                return;
            
            _animation.PlayHide(WindowHidden);
        }
    }
}