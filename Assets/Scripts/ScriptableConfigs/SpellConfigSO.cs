using System;
using System.Collections.Generic;
using MainGame.Abilities.Spells;
using UnityEngine;
using MainGame.Stats.ConcreteStat;

namespace MainGame.ScriptableConfigs
{
    [CreateAssetMenu(fileName = "SpellConfig", menuName = "Spell/SpellConfig")]
    public class SpellConfigSO : ScriptableObject
    {
        [field: SerializeField] public SpellType SpellType { get; private set; }
        [field: SerializeField] public SpellView SpellPrefab { get; private set; }
        [field: SerializeField] public ParticleSystem MuzzleFlashVfx { get; private set; }
        [field: SerializeField] public ParticleSystem HitVfx { get; private set; }
        [field: SerializeField] public Sprite SpellSprite { get; private set; }
        
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: SerializeField] public float CastSpeed { get; private set; }
        [field: SerializeField] public float Knockback { get; private set; }
        [field: SerializeField] public float ProjectileSpeed { get; private set; }
        [field: SerializeField] public int Bounce { get; private set; }
        [field: SerializeField] public float Radius { get; private set; }
        
        [field: SerializeField] public string AnimationClipName { get; private set; }
        
        [field: SerializeField] public SpellCastPosition CastPosition { get; private set; }
        [field: SerializeField] public MovementType MovementType { get; private set; }
        public int AnimationHash { get; private set; }

        private void Awake() => AnimationHash = Animator.StringToHash(AnimationClipName);
        private void OnValidate() => AnimationHash = Animator.StringToHash(AnimationClipName);

        public Dictionary<Type, object> GetStatLibrary()
        {
            Dictionary<Type, object> statsDict = new()
            {
                {typeof(DamageStat), new DamageStat(Damage)},
                {typeof(KnockbackStat), new KnockbackStat(Knockback)},
                {typeof(BounceStat), new BounceStat(Bounce)},
                {typeof(ProjectileSpeedStat), new ProjectileSpeedStat(ProjectileSpeed)},
                {typeof(RadiusStat), new RadiusStat(Radius)},
                {typeof(Cooldown), new Cooldown(Cooldown)},
                {typeof(CastSpeed), new CastSpeed(CastSpeed)}
            };

            return statsDict;
        }
    }

    public enum SpellCastPosition
    {
        FromCastersHands = 0,
        ClickedPosition = 1
    }

    public enum MovementType
    {
        NoForce = 0,
        PositiveAdditiveForce = 1,
        NegativeAdditiveForce = 2,
        ConstantForce = 3
    }

    public enum SpellType
    {
        Fireball = 0,
        EnergyBlast = 1,
        LightningBolt = 2
    }
}
