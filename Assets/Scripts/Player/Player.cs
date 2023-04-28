using System;
using System.Collections.Generic;
using MainGame.ScriptableConfigs;
using MainGame.Stats.Interfaces;
using NaughtyAttributes;
using UnityEngine;

namespace MainGame.Player
{
    public class Player : MonoBehaviour, IStatHolder
    {
        [Expandable] [Required]
        [SerializeField] private CharacterStatsSO _playerDefaultStats;
        
        private Dictionary<Type, object> _statsDict;

        void IStatHolder.InitializeStatLibrary()
        {
            _statsDict = _playerDefaultStats.GetStatLibrary();
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
