using UnityEngine;
using UnityEngine.UI;

namespace Code.PresentationModel
{
    public class EnemyHpBar : HpBar
    {
        [SerializeField] private Image _backgroundValue;
        [SerializeField] private float _speed;

        private void Update()
        {
            if (_backgroundValue.fillAmount > _currentValue)
            {
                _backgroundValue.fillAmount = GetBackgroundProgress();
            }
        }

        private float GetBackgroundProgress()
        {
            return Mathf.Lerp(_backgroundValue.fillAmount, _currentValue, _speed * Time.deltaTime);
        }
    }
}