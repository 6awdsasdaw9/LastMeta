using System;

namespace Code.Services.CurrencyServices
{
    public class MoneyStorage
    {
        private int _current;
        public Action<int> OnChangeValue;
        public MoneyStorage()
        {
            
        }

        public void Set(int value)
        {
            _current = value;
        }

        public void Add(int value)
        {
            _current += value;
            OnChangeValue?.Invoke(_current);
        }

        public void Subtract(int value)
        {
            _current -= value;
            if (_current <= 0)
            {
                _current = 0;
            }
            OnChangeValue?.Invoke(_current);
        }
    }
}