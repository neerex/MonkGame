using MainGame.Stats;
using MainGame.Stats.Interfaces;
using UnityEngine;

namespace MainGame.Pickups
{
    public class DamageUpPickup : PickupEffect
    {
        [SerializeField] private float _multiplyDamage;
        [SerializeField] private float _addFlatDamage;
        
        public override void ApplyEffect(GameObject go)
        {
            if (go.TryGetComponent(out ICharacterStatHolder statHolder))
            {
                if(statHolder.GetStat(out DamageStat damageStat))
                {
                    var modifier1 = new StatModifier<float>(_addFlatDamage, 
                        StatModifierType.Flat, 
                        (a, b) => a + b, 
                        this);
                    
                    var modifier2 = new StatModifier<float>(_multiplyDamage, 
                        StatModifierType.PercentMult, 
                        (a, b) => a * (1 + b), 
                        this);
                    
                    damageStat.AddModifier(modifier1);
                    damageStat.AddModifier(modifier2);
                }
            }
        }
    }
}