using System;
using MainGame.Damage;
using UnityEngine;

namespace MainGame.ScriptableConfigs
{
    [CreateAssetMenu(fileName = "SpellConfig", menuName = "Spell/SpellConfig")]
    public class SpellConfigSO : ScriptableObject
    {
        [field: SerializeField] public DamageSource SpellPrefab { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public string AnimationString { get; private set; }
        [field: SerializeField] public SpellCastPosition CastPosition { get; private set; }
        [field: SerializeField] public MovementType MovementType { get; private set; }
        [field: SerializeField] public SpellType SpellType { get; private set; }
        [field: SerializeField] public int AnimationHash { get; private set; }
        
        private void OnValidate()
        {
            AnimationHash = Animator.StringToHash(AnimationString);
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
        BlastNova = 1,
        LightningBolt = 2
    }
}
