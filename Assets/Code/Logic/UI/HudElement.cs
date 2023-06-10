using UnityEngine;

namespace Code.UI
{
    public abstract class HudElement : MonoBehaviour
    {
        [SerializeField] protected RectTransform Body;

        public virtual void Show()
        {
            Body.gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            Body.gameObject.SetActive(false);
        }
    }
}