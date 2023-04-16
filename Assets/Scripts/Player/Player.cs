using System;
using System.Collections.Generic;
using MainGame.Stats;
using UnityEngine;

namespace MainGame.Player
{
    public class Player : MonoBehaviour, ICharacterStatHolder
    {
        private readonly Dictionary<Type, object> _statsDict = new();

        private void Awake()
        {
            PopulateStatLibrary();
        }

        private void PopulateStatLibrary()
        {
            _statsDict.Add(typeof(HealthStat), new HealthStat(10));
            _statsDict.Add(typeof(DamageStat), new DamageStat(150));
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
