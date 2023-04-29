using System;
using System.Collections.Generic;
using MainGame.ScriptableConfigs;
using MainGame.Stats.Interfaces;
using NaughtyAttributes;
using UnityEngine;

namespace MainGame.Entities.Player
{
    public class Player : MonoBehaviour, IStatHolder
    {
        [Expandable] [Required]
        [SerializeField] private CharacterStatsConfigSO _playerDefaultStatsConfig;
        
        private Dictionary<Type, object> _statsDict;

        void IStatHolder.InitializeStatLibrary()
        {
            _statsDict = _playerDefaultStatsConfig.GetStatLibrary();
        }

        bool IStatHolder.GetStat<T>(out T stat)
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
