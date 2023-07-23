using System;
using UnityEngine;

namespace Code.UI.HeadUpDisplay.Windows.HudElementsAnimation
{
    public abstract class WindowAnimation : MonoBehaviour
    {
        public abstract void PlayShow(Action WindowShowed = null);
        public abstract void PlayHide(Action WindowHidden = null);
        public bool IsPlay { get; protected set; }
    }
}