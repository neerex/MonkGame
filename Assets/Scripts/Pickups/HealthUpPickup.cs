using MainGame.Stats;
using MainGame.Stats.Interfaces;
using UnityEngine;

namespace MainGame.Pickups
{
    public class HealthUpPickup : PickupEffect
    {
        [SerializeField] private float _multiplyHealth;
        [SerializeField] private float _addFlatHealth;
        
        public override void ApplyEffect(GameObject go)
        {
            if (go.TryGetComponent(out ICharacterStatHolder statHolder))
            {
                if(statHolder.GetStat(out MaxHealthStat healthStat))
                {
                    var modifier1 = new StatModifier<float>(_addFlatHealth, 
                        StatModifierType.Flat, 
                        (a, b) => a + b, 
                        this);
                    
                    var modifier2 = new StatModifier<float>(_multiplyHealth, 
                        StatModifierType.PercentMult, 
                        (a, b) => a * (1 + b), 
                        this);
                    
                    healthStat.AddModifier(modifier1);
                    healthStat.AddModifier(modifier2);
                }
            }
        }
    }
}