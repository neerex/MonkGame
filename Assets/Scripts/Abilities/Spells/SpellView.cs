using UnityEngine;

namespace MainGame.Abilities.Spells
{
    public abstract class SpellView : MonoBehaviour
    {
        protected Spell Spell;

        public virtual void Init(Spell spell)
        {
            Spell = spell;
        }
    }
}