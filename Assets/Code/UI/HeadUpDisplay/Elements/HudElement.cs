using UnityEngine;

namespace Code.UI.HeadUpDisplay.Elements
{
    public abstract class HudElement : MonoBehaviour
    {
        [SerializeField] protected RectTransform Body;
        public bool IsOpen { get; private set; }

        public virtual void Show()
        {
            IsOpen = true;
            Body?.gameObject?.SetActive(true);
        }

        public virtual void Hide()
        {
            IsOpen = false;
            Body?.gameObject?.SetActive(false);
        }
    }
}