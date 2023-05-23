using MainGame.Abilities.Spells;
using MainGame.Infrastructure.EntityFactories.SpellFactory.Interfaces;
using MainGame.ScriptableConfigs;
using UnityEngine;
using Zenject;

namespace MainGame.Pickups
{
    public class SpellPickup : PickupEffect
    {
        [SerializeField] private SpellType _spellType;
        private ISpellFactory _spellFactory;

        [Inject]
        public void Construct(ISpellFactory spellFactory)
        {
            _spellFactory = spellFactory;
        }
        
        public override void ApplyEffect(GameObject go)
        {
            if (go.TryGetComponent(out ISpellBookHolder spellBookHolder))
            {
                Spell spell = _spellFactory.CreateSpell(_spellType);
                spellBookHolder.SpellBook.TryAddSpell(spell);
            }
        }
    }
}