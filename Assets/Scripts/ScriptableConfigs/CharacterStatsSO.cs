using System;
using System.Collections.Generic;
using MainGame.Stats;
using MainGame.Stats.ConcreteStat;
using UnityEngine;

namespace MainGame.ScriptableConfigs
{
    [CreateAssetMenu(fileName = "CharacterStats", menuName = "Stats/CharacterStats")]
    public class CharacterStatsSO : ScriptableObject
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _damage;
        [SerializeField] private float _movementSpeed;
        
        public Dictionary<Type, object> GetStatLibrary()
        {
            Dictionary<Type, object> statsDict = new()
            {
                {typeof(MaxHealthStat), new MaxHealthStat(_maxHealth)},
                {typeof(DamageStat), new DamageStat(_damage)},
                {typeof(MovementSpeedStat), new MovementSpeedStat(_movementSpeed)}
            };

            return statsDict;
        }
    }
}
