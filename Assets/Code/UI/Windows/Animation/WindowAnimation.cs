using UnityEngine;

namespace Code.UI.Windows
{
    public abstract class WindowAnimation : MonoBehaviour
    {
        public abstract void PlayShow();
        public abstract void PlayHide();
        public bool IsPlay { get; protected set; }
    }
}