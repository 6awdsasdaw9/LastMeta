using UnityEngine;
using UnityEngine.UI;

namespace Code.PresentationModel
{
    public class SliderController : HudElement
    {
        [SerializeField] private Slider _slider;
    
        public void SetValue(float normalize)
        {
            _slider.value = normalize;
        }
    }
}
