using System;

namespace MainGame.Stats
{
    public class StatModifier<T> : IComparable<StatModifier<T>>
    {
        public readonly object Source;
        
        private readonly T _value;
        private readonly int _order;
        private readonly Func<T, T, T> _calculation;

        public StatModifier(T value, StatModifierType type, Func<T,T,T> calculation, object source = null)
        {
            _value = value;
            _calculation = calculation;
            _order = (int)type;
            Source = source;
        }

        public T Calculate(T valueToModify) => _calculation(valueToModify, _value);
        public int CompareTo(StatModifier<T> compareTo) => _order - compareTo._order;
    }
}