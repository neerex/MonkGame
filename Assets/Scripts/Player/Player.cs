using System;
using System.Collections.Generic;
using MainGame.ScriptableConfigs;
using MainGame.Stats;
using UnityEngine;

namespace MainGame.Player
{
    public class Player : MonoBehaviour, ICharacterStatHolder
    {
        [SerializeField] private CharacterStatsSO _playerDefaultStats;
        
        private readonly Dictionary<Type, object> _statsDict = new();

        private void Awake()
        {
            PopulateStatLibrary();
        }

        private void PopulateStatLibrary()
        {
            _statsDict.Add(typeof(HealthStat), new HealthStat(_playerDefaultStats.Health));
            _statsDict.Add(typeof(DamageStat), new DamageStat(_playerDefaultStats.Damage));
            _statsDict.Add(typeof(MovementSpeedStat), new MovementSpeedStat(_playerDefaultStats.MovementSpeed));
        }

        public bool GetStat<T>(out T stat)
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
