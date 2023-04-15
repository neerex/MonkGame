using System;

namespace MainGame.Stats
{
    public class Observable<T>
    {
        private T _value;
        /// <summary> <b>Old value</b> and <b>NewValue</b> </summary>
        public event Action<T,T> Changed;

        public T Value 
        {
            get => _value;
            set 
            {
                if (!_value.Equals(value))
                {
                    T oldValue = _value;
                    _value = value;
                    Changed?.Invoke(oldValue, value);
                }
            }        
        }

        public void SetWithoutCallback(T newValue) => _value = newValue;
        public static implicit operator T(Observable<T> mutable) => mutable.Value;
    }
}