using System;
using System.Linq;
using MainGame.ScriptableConfigs;
using UnityEngine;
using Logger = MainGame.Utilities.Logger;

namespace MainGame.Abilities.Spells
{
    public class SpellBook
    {
        private readonly Spell[] _spells;
        public readonly int Capacity;
        public Spell this[int index] => _spells[index];

        public event Action<SpellBook> OnSpellPositionInTheBookChanged;
        public event Action<SpellBook, int> OnSpellAdded;

        public SpellBook(int capacity)
        {
            Capacity = capacity;
            _spells = new Spell[capacity];
        }
        
        public bool TryAddSpell(Spell spell)
        {
            if (spell == null) return false;
            
            if (_spells.Any(s => s != null && s.SpellConfig == spell.SpellConfig))
            {
                // probably level up spell if pick up same one in the future iteration on this system
                Logger.Log($"Spell already exist in SpellBook");
                return false;
            }
            
            if (GetFreeSlot(out int index))
            {
                _spells[index] = spell;
                OnSpellAdded?.Invoke(this, index);
                Logger.Log($"Spell added in SpellBook. SpellName: {spell.GetType().Name}", Color.green);
                return true;
            }

            return false;
        }

        public void ChangeSpellPlaces(int indexFrom, int indexTo)
        {
            (_spells[indexFrom], _spells[indexTo]) = (_spells[indexTo], _spells[indexFrom]);
            OnSpellPositionInTheBookChanged?.Invoke(this);
        }

        private bool GetFreeSlot(out int index)
        {
            index = -1;
            for (int i = 0; i < _spells.Length; i++)
            {
                if(_spells[i] != null) 
                    continue;
                
                index = i;
                return true;
            }

            return false;
        }
    }
}