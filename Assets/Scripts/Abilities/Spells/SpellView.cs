using UnityEngine;

namespace MainGame.Abilities.Spells
{
    public abstract class SpellView : MonoBehaviour
    {
        protected Spell Spell;
        protected SpellCastInfo CastInfo;

        public virtual void Init(Spell spell, SpellCastInfo spellCastInfo)
        {
            Spell = spell;
            CastInfo = spellCastInfo;
        }
    }
}