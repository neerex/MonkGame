using MainGame.Abilities.Spells;
using MainGame.ScriptableConfigs;
using UnityEngine;

namespace MainGame.Pickups
{
    public class SpellPickup : PickupEffect
    {
        [SerializeField] private SpellConfigSO _spellConfigSo;

        public override void ApplyEffect(GameObject go)
        {
            if (go.TryGetComponent(out SpellController spellController))
            {
                
            }
        }
    }
}