using System;
using System.Collections;
using MainGame.CharacterResources.Interfaces;
using MainGame.Stats;
using MainGame.Stats.Interfaces;
using MainGame.Utilities;
using NaughtyAttributes;
using UnityEngine;
using Logger = MainGame.Utilities.Logger;

namespace MainGame.CharacterResources
{
    [RequireComponent(typeof(ICharacterStatHolder))]
    public class Health : MonoBehaviour, IHealth, IStatsReader
    {
        private ICharacterStatHolder _characterStatHolder;
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

        public event ValueChangedDelegate<float> ValueChanged;

        private void Awake()
        {
            _characterStatHolder = GetComponent<ICharacterStatHolder>();
        }

        private void OnDestroy()
        {
            if(_maxHealthStat != null) 
                _maxHealthStat.ValueChanged -= OnMaxValueChanged;
        }

        void IStatsReader.InitializeStats()
        {
            _characterStatHolder.GetStat(out _maxHealthStat);
            Current = Max;
            _maxHealthStat.ValueChanged += OnMaxValueChanged;
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
                ValueChanged?.Invoke(oldValue, Current);
                Logger.Log($"Current Health: {Current}", gameObject, Color.green);
            }
        }
        
        private void OnMaxValueChanged(float oldValue, float newValue) => 
            ValueChanged?.Invoke(Current, Current);
    }
}