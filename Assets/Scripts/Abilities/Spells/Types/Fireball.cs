using System;
using MainGame.Collisions.Triggers;
using MainGame.Damage.Effects;
using MainGame.Stats.ConcreteStat;
using NaughtyAttributes;
using UnityEngine;

namespace MainGame.Abilities.Spells.Types
{
    [RequireComponent(typeof(Rigidbody))]
    public class Fireball : SpellView
    {
        [Required][SerializeField] private SphereTriggerObserver _triggerObserver;
        [SerializeField] private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            Destroy(gameObject,10);
        }

        public override void Init(Spell spell, SpellCastInfo spellCastInfo)
        {
            base.Init(spell, spellCastInfo);
            _triggerObserver.TriggerEnter += ApplyCollisionEffects;
            if (Spell.SpellConfig.MuzzleFlashVfx != null)
            {
                Instantiate(Spell.SpellConfig.MuzzleFlashVfx, transform.position, Quaternion.identity);
            }
            
            Spell.GetStat(out ProjectileSpeedStat stat);
            if (stat != null)
            {
                _rigidbody.AddForce(transform.forward * stat.Value, ForceMode.VelocityChange);
            }
        }

        private void OnDestroy()
        {
            _triggerObserver.TriggerEnter -= ApplyCollisionEffects;
        }

        private void ApplyCollisionEffects(Collider other)
        {
            if (other.TryGetComponent(out IPushable pushable))
            {
                Spell.GetStat(out KnockbackStat stat);
                if (stat != null)
                {
                    pushable.Push(transform.forward + Vector3.up * 0.2f, stat.Value);
                } 
            }
            
            if (other.TryGetComponent(out IDamageable damageable))
            {
                Spell.GetStat(out DamageStat stat);
                if (stat != null)
                {
                    damageable.TakeDamage(stat.Value);
                }
            }

            if (Spell.SpellConfig.HitVfx != null)
            {
                Instantiate(Spell.SpellConfig.HitVfx, transform.position, Quaternion.LookRotation(Vector3.back, Vector3.up));
            }
            
            Destroy(gameObject);
        }
    }
}