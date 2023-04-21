using System;
using System.Collections.Generic;
using MainGame.ScriptableConfigs;
using MainGame.Stats;
using MainGame.Stats.Interfaces;
using NaughtyAttributes;
using UnityEngine;

namespace MainGame.Player
{
    public class Player : MonoBehaviour, ICharacterStatHolder
    {
        [Expandable] [Required]
        [SerializeField] private CharacterStatsSO _playerDefaultStats;
        
        private readonly Dictionary<Type, object> _statsDict = new();

        void ICharacterStatHolder.InitializeStatLibrary()
        {
            _statsDict.Add(typeof(MaxHealthStat), new MaxHealthStat(_playerDefaultStats.Health));
            _statsDict.Add(typeof(DamageStat), new DamageStat(_playerDefaultStats.Damage));
            _statsDict.Add(typeof(MovementSpeedStat), new MovementSpeedStat(_playerDefaultStats.MovementSpeed));
        }

        bool ICharacterStatHolder.GetStat<T>(out T stat)
        {
            if(_statsDict.TryGetValue(typeof(T), out object s))
            {
                stat = (T)s;
                return true;
            }

            stat = default;
            return false;
        }
    }
}
