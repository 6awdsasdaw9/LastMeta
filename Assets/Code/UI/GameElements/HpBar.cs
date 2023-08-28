using Code.Debugers;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.GameElements
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] protected Image _value;
        protected float _currentValue;

        public void SetValue(float current, float max)
        {
            _currentValue = current / max;
            _value.fillAmount = _currentValue;
            Logg.ColorLog($"Hp bar({gameObject.name}) value = {_currentValue}",ColorType.Orange);
        }
    }
}