using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class HpBar : MonoBehaviour
    {
        public Image _image;

        public void SetValue(float current, float max) =>
            _image.fillAmount = current / max;
    }
}