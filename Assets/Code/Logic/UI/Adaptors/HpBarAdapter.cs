using Code.Character.Interfaces;
using UnityEngine;

namespace Code.UI.Adaptors
{
    public class HpBarAdapter : MonoBehaviour
    {
        protected HpBar _hpBar;
        private IHealth _health;
        
        protected  void Start()
        {
            _health = GetComponent<IHealth>();
            _health.HealthChanged += UpdateHpBar;
        }
        
        private void OnDestroy()
        {
            if (_health != null)
                _health.HealthChanged -= UpdateHpBar;
        }
        
        private void UpdateHpBar()
        {
            _hpBar.SetValue(_health.Current, _health.Max);
        }
    }
}