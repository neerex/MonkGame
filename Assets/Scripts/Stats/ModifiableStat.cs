using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MainGame.Utilities;
using UnityEngine;
using Logger = MainGame.Utilities.Logger;

namespace MainGame.Stats
{
	public abstract class ModifiableStat<T>
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
				return _value;
			}
		}

		private readonly List<StatModifier<T>> _statModifiers;
		public readonly ReadOnlyCollection<StatModifier<T>> StatModifiers;
		
		public event Action<StatModifier<T>> ModifierAdded;
		public event Action<StatModifier<T>> ModifierRemoved;
		public event ValueChangedDelegate<T> ValueChanged;

		protected ModifiableStat(T baseValue)
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
			IEnumerable<StatModifier<T>> modifiersToRemove = _statModifiers.Where(m => m.Source == source);
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
			T oldValue = _value;
			T finalValue = _baseValue;
			_statModifiers.Sort();

			foreach (var mod in _statModifiers)
			{
				finalValue = mod.Calculate(finalValue);
			}
			
			_value = finalValue;
			_isDirty = false;
			Logger.Log($"CurrMax :{_value}", GetType().Name, Color.cyan);
			ValueChanged?.Invoke(oldValue, _value);
		}
    }
}