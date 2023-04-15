using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MainGame.Stats
{
    public class CharacterStat<T>
    {
	    private readonly T _baseValue;

        private bool _isDirty = true;

		private T _value;
		public T Value 
		{
			get 
			{
				if (!_isDirty) return _value;
				CalculateFinalValue();
				_isDirty = false;
				ValueChanged?.Invoke(_value);
				return _value;
			}
			private set
			{
				_value = value;
				ValueChanged?.Invoke(_value);
			}
		}

		private readonly List<StatModifier<T>> _statModifiers;
		public readonly ReadOnlyCollection<StatModifier<T>> StatModifiers;
		
		public event Action<StatModifier<T>> ModifierAdded;
		public event Action<StatModifier<T>> ModifierRemoved;
		public event Action<T> ValueChanged;

		protected CharacterStat(T baseValue)
		{
			_baseValue = baseValue;
			_statModifiers = new List<StatModifier<T>>();
			StatModifiers = _statModifiers.AsReadOnly();
		}

		public void AddModifier(StatModifier<T> mod)
		{
			_isDirty = true;
			_statModifiers.Add(mod);
			ModifierAdded?.Invoke(mod);
			CalculateFinalValue();
		}

		public void RemoveModifier(StatModifier<T> mod)
		{
			if (_statModifiers.Remove(mod))
			{
				_isDirty = true;
				ModifierRemoved?.Invoke(mod);
				CalculateFinalValue();
			}
		}

		public void RemoveAllModifiersFromSource(object source)
		{
			int numRemovals = 0;
			var modifiersToRemove = _statModifiers.Where(m => m.Source == source);
			foreach (var modifier in modifiersToRemove)
			{
				RemoveModifier(modifier);
				numRemovals++;
			}

			if (numRemovals > 0)
			{
				_isDirty = true;
				CalculateFinalValue();
			}
		}

		private void CalculateFinalValue()
		{
			T finalValue = _baseValue;
			_statModifiers.Sort();

			foreach (var mod in _statModifiers)
			{
				finalValue = mod.Calculate(finalValue);
			}

			Value = finalValue;
		}
    }
}