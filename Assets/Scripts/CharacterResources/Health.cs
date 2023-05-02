using System;
using System.Collections;
using MainGame.CharacterResources.Interfaces;
using MainGame.Stats.ConcreteStat;
using MainGame.Stats.Interfaces;
using MainGame.Utilities;
using NaughtyAttributes;
using UnityEngine;
using Logger = MainGame.Utilities.Logger;

namespace MainGame.CharacterResources
{
    [RequireComponent(typeof(IStatHolder))]
    public class Health : MonoBehaviour, IHealth, IStatsReader
    {
        private IStatHolder _statHolder;
        private MaxHealthStat _maxHealthStat;
        
        public float Current { get; private set; }
        public float Max
        {
            get
            {
                if(_maxHealthStat != null)
                    return _maxHealthStat.Value;

                return 0f;
            }
        }

        public event ValueChangedDelegate<float> OnValueChanged;

        private void Awake()
        {
            _statHolder = GetComponent<IStatHolder>();
        }

        private void OnDestroy()
        {
            if(_maxHealthStat != null) 
                _maxHealthStat.OnValueChanged -= OnMaxOnValueChanged;
        }

        void IStatsReader.InitializeStats()
        {
            _statHolder.GetStat(out _maxHealthStat);
            Current = Max;
            _maxHealthStat.OnValueChanged += OnMaxOnValueChanged;
        }

        [Button("Take Damage")] 
        public IEnumerator Damage()
        {
            for (float i = 0; i < 10; i++)
            {
                TakeDamage(10);
                yield return new WaitForSeconds(0.2f);
            }
        }

        [Button("Heal")] 
        public IEnumerator Heal()
        {
            for (float i = 0; i < 10; i++)
            {
                Heal(15);
                yield return new WaitForSeconds(0.2f);
            }
        }

        public void TakeDamage(float amount) => 
            ChangeCurrentHealthValue(-amount);

        public void Heal(float amount) => 
            ChangeCurrentHealthValue(amount);

        private void ChangeCurrentHealthValue(float amount)
        {
            float oldValue = Current;
            Current += amount;
            Current = Math.Clamp(Current, 0, _maxHealthStat.Value);
            if (!Mathf.Approximately(oldValue, Current))
            {
                OnValueChanged?.Invoke(oldValue, Current);
                Logger.Log($"Current Health: {Current}", gameObject, Color.green);
            }
        }
        
        private void OnMaxOnValueChanged(float oldValue, float newValue) => 
            OnValueChanged?.Invoke(Current, Current);
    }
}