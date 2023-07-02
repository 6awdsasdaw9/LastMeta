using System;

namespace Code.Services.CurrencyServices
{
    public class MoneyStorage
    {
        public int Current { get; private set; }
        public Action<int> OnChangeValue;
        
        public void Set(int value)
        {
            Current = value;
            OnChangeValue?.Invoke(Current);
        }

        public void Add(int value)
        {
            Current += value;
            OnChangeValue?.Invoke(Current);
        }

        public void Subtract(int value)
        {
            Current -= value;
            if (Current <= 0)
            {
                Current = 0;
            }
            OnChangeValue?.Invoke(Current);
        }
    }
}