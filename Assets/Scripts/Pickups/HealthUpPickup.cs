using MainGame.Stats;
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
                if(statHolder.GetStat(out HealthStat damageStat))
                {
                    var modifier1 = new StatModifier<float>(_addFlatHealth, 
                        StatModifierType.Flat, 
                        (a, b) => a + b, 
                        this);
                    
                    var modifier2 = new StatModifier<float>(_multiplyHealth, 
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