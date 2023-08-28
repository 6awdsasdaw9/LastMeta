using Code.Debugers;
using Code.Logic.Common.Interfaces;
using UnityEngine;

namespace Code.UI.GameElements.Adapters
{
    public abstract class HealthBarAdapter : MonoBehaviour
    {
        protected abstract HpBar hpBar { get; }
        protected abstract ICharacterHealth health { get; }

        protected virtual void OnEnable()
        {
            SubscribeToEvents(true);
        }

        protected virtual void OnDisable()
        {
            SubscribeToEvents(false);
        }

        private void UpdateHpBar()
        {
            Logg.ColorLog($"{gameObject.name} UpdateHpBar", ColorType.Red);
            hpBar.SetValue(health.Current, health.Max);
        }

        protected virtual void SubscribeToEvents(bool flag)
        {
            if (health == null) return;
            if (flag)
            {
                health.OnHealthChanged += UpdateHpBar;
            }
            else
            {
                health.OnHealthChanged -= UpdateHpBar;
            }
        }
    }
}