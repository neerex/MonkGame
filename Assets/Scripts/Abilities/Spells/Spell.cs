using System;
using System.Collections;
using System.Collections.Generic;
using MainGame.ScriptableConfigs;
using MainGame.Stats.Interfaces;
using MainGame.Utilities;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MainGame.Abilities.Spells
{
    public class Spell : IStatHolder
    {
        private float _startOfCast;
        private float _endOfCast;
        private SpellCastInfo _spellCastInfo;
        private Dictionary<Type, object> _statsDict;
        public SpellConfigSO SpellConfig { get; private set; }

        public bool IsCasting { get; private set; }

        //TODO: Add timer class for cooldown
        
        public Spell(SpellConfigSO spellConfig)
        {
            SpellConfig = spellConfig;
            ((IStatHolder) this).InitializeStatLibrary();
        }

        void IStatHolder.InitializeStatLibrary()
        {
            _statsDict = SpellConfig.GetStatLibrary();
        }

        public bool GetStat<T>(out T stat)
        {
            if(_statsDict.TryGetValue(typeof(T), out object s))
            {
                stat = (T)s;
                return true;
            }

            stat = default;
            return false;
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
            SpellView spellView = Object.Instantiate(
                SpellConfig.SpellPrefab, 
                castInfo.CasterHandsForward.position, 
                Quaternion.LookRotation(castInfo.CasterHandsForward.forward.FlatY()));
            
            spellView.Init(this);
            yield return null;
        }
    }
}