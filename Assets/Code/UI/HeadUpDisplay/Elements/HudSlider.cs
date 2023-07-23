using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.HeadUpDisplay.HudElements
{
    public class HudSlider : HudElement
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private bool _isTransmitsValue;
        
        public Action<float> OnChangedSliderValue;

        private void OnEnable()
        {
            if (_isTransmitsValue)
            {
                SetEventOnChangesValue();
            }
        }

        public void SetValue(float normalize)
        {
            _slider.value = normalize;
        }
        
        private void SetEventOnChangesValue()
        {
            _slider.onValueChanged.AddListener(ChangeSliderValueEvent);
        }
        
        private void ChangeSliderValueEvent(float sliderValue)
        {
            OnChangedSliderValue?.Invoke(sliderValue);
        }
        
    }
}
