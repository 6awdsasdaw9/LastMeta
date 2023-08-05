using Code.Logic.Common.Interfaces;
using UnityEngine;

namespace Code.UI.GameElements.Adapters
{
    public class HpBarAdapter : MonoBehaviour
    {
        protected HpBar _hpBar;
        private ICharacterHealth _health;
        
        protected  void Start()
        {
            _health = GetComponent<ICharacterHealth>();
            _health.OnHealthChanged += UpdateHpBar;
        }
        
        private void OnDestroy()
        {
            if (_health != null)
                _health.OnHealthChanged -= UpdateHpBar;
        }
        
        private void UpdateHpBar()
        {
            _hpBar.SetValue(_health.Current, _health.Max);
        }
    }
}