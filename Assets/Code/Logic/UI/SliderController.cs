using System.Collections;
using System.Collections.Generic;
using Code.UI;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SliderController : HudElement
{
    [SerializeField] private Slider _slider;
    
    public void SetValue(float normalize)
    {
        _slider.value = normalize;
    }
}
