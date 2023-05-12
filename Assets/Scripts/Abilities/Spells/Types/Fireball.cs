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
        [SerializeField] private float _maxFlightTime = 10f;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            Destroy(gameObject,_maxFlightTime);
        }

        private void OnDestroy()
        {
            _triggerObserver.TriggerEnter -= ApplyCollisionEffects;
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
                Instantiate(Spell.SpellConfig.HitVfx, transform.position, Quaternion.LookRotation(-transform.forward, Vector3.up));
            }

            _rigidbody.velocity = Vector3.zero;
            Destroy(gameObject,1f);
        }
    }
}