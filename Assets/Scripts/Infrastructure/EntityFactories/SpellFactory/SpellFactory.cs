using System.Collections.Generic;
using System.Linq;
using MainGame.Abilities.Spells;
using MainGame.Infrastructure.EntityFactories.SpellFactory.Interfaces;
using MainGame.ScriptableConfigs;
using Logger = MainGame.Utilities.Logger;

namespace MainGame.Infrastructure.EntityFactories.SpellFactory
{
    public class SpellFactory : ISpellFactory
    {
        private readonly Dictionary<SpellType, SpellConfigSO> _spellConfigs;
        private readonly Spell.Factory _spellFactory;
        
        public SpellFactory(SpellConfigListSO spellConfigListSo, Spell.Factory spellFactory)
        {
            _spellConfigs = spellConfigListSo.SpellConfigs.ToDictionary(x => x.SpellType, x => x);
            _spellFactory = spellFactory;
        }

        public Spell CreateSpell(SpellType spellType)
        {
            if (_spellConfigs.TryGetValue(spellType, out SpellConfigSO config))
            {
                Spell spell = _spellFactory.Create(config);
                return spell;
            }
            
            Logger.LogError($"There is no {spellType} type of spell in SpellConfigListSO");
            return default;
        }
    }
}