using MainGame.Abilities.Spells;
using MainGame.ScriptableConfigs;

namespace MainGame.Infrastructure.EntityFactories.SpellFactory.Interfaces
{
    public interface ISpellFactory
    {
        Spell CreateSpell(SpellType spellType);
    }
}