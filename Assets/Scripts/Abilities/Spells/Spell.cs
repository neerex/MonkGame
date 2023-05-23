using System;
using System.Collections;
using System.Collections.Generic;
using MainGame.Infrastructure.Services.Timer;
using MainGame.ScriptableConfigs;
using MainGame.Stats.Interfaces;
using MainGame.Utilities;
using UnityEngine;
using Zenject;
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
        public bool CanCast;

        //TODO: Add timer class for cooldown
        
        public Spell(SpellConfigSO spellConfig, XTimer.Factory timer)
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
            Vector3 pos = SpellConfig.CastPosition switch
            {
                SpellCastPosition.FromCastersHands => castInfo.CasterHands.position,
                SpellCastPosition.ClickedPosition => castInfo.ClickedPosition.point + castInfo.ClickedPosition.normal * 0.5f,
                _ => Vector3.zero
            };

            var rot = SpellConfig.CastPosition switch
            {
                SpellCastPosition.FromCastersHands => Quaternion.LookRotation(castInfo.CasterHands.forward.FlatY()),
                SpellCastPosition.ClickedPosition => Quaternion.identity,
                _ => Quaternion.identity
            };
            
            SpellView spellView = Object.Instantiate(
                SpellConfig.SpellPrefab, 
                pos, 
                rot
            );
            
            spellView.Init(this, castInfo);
            yield return null;
        }
        
        public class Factory : PlaceholderFactory<SpellConfigSO, Spell> { }
    }
}