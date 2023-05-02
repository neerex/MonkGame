using System;
using System.Collections;
using MainGame.Damage.Effects;
using MainGame.Stats.ConcreteStat;
using UnityEngine;
using Logger = MainGame.Utilities.Logger;
using Random = UnityEngine.Random;

namespace MainGame.Abilities.Spells.Types
{
    public class EnergyBlast : SpellView
    {
        [SerializeField] [Range(0, 1f)] private float _delayBeforeDamageApplied = 0;
        [SerializeField] private int _maxTargetsToEffect;
        
        private Collider[] _colliders;
        private RadiusStat _radiusStat;
        private DamageStat _damageStat;
        private KnockbackStat _knockback;

        private void Awake()
        {
            _colliders = new Collider[_maxTargetsToEffect];
        }

        public override void Init(Spell spell, SpellCastInfo spellCastInfo)
        {
            base.Init(spell, spellCastInfo);
            
            spell.GetStat(out _radiusStat);
            spell.GetStat(out _damageStat);
            spell.GetStat(out _knockback);

            transform.localScale *= _radiusStat.Value;
            
            StartCoroutine(ApplyEffects());
        }

        private IEnumerator ApplyEffects()
        {
            yield return new WaitForSeconds(_delayBeforeDamageApplied);
            int overlaps = Physics.OverlapSphereNonAlloc(transform.position, _radiusStat.Value, _colliders, CastInfo.LayerToDamage);
            for (int i = 0; i < overlaps; i++)
            {
                if(_colliders[i] is null) continue;

                if (_colliders[i].TryGetComponent(out IPushable pushable))
                {
                    Vector3 pushDirection = pushable.Transform.position - transform.position + Vector3.up * Random.Range(0.2f, 1.5f);
                    pushable.Push(pushDirection, _knockback.Value);
                }
                
                if (_colliders[i].TryGetComponent(out IDamageable damageable))
                    damageable.TakeDamage(_damageStat.Value);
            }
            
            Destroy(gameObject, 1f);
        }

        private void OnDrawGizmos()
        {
            if(_radiusStat == null) return;
            Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.3f);
            Gizmos.DrawSphere(transform.position, _radiusStat.Value);
        }
    }
}
