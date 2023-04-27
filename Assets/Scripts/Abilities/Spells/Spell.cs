using System.Collections;
using MainGame.ScriptableConfigs;
using UnityEngine;

namespace MainGame.Abilities.Spells
{
    public class Spell
    {
        private float _startOfCast;
        private float _endOfCast;
        private SpellCastInfo _spellCastInfo;
        public SpellConfigSO SpellConfig { get; private set; }
        
        public bool IsCasting { get; private set; }

        //TODO: Add timer class for cooldown
        
        public Spell(SpellConfigSO spellConfig)
        {
            SpellConfig = spellConfig;
        }

        protected virtual void PreCast()
        {
            IsCasting = true;
            _startOfCast = Time.time;
        }

        public IEnumerator BeginCast(SpellCastInfo castInfo)
        {
            _spellCastInfo = castInfo;
            
            PreCast();
            yield return CastSpell(castInfo);
            EndCast();
        }

        protected virtual void EndCast()
        {
            _endOfCast = Time.time;
            IsCasting = false;
        }

        protected virtual IEnumerator CastSpell(SpellCastInfo castInfo)
        {
            yield return null;
        }
    }
}