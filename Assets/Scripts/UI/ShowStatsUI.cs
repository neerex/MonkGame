using MainGame.Stats;
using TMPro;
using UnityEngine;

namespace MainGame.UI
{
    public class ShowStatsUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private TextMeshProUGUI _damageText;

        private HealthStat _healthStat;
        private DamageStat _damageStat;

        private ICharacterStatHolder _characterStatHolder;
        
        private void Awake()
        {
            _characterStatHolder = GetComponentInParent<ICharacterStatHolder>();
        }

        private void OnEnable()
        {
            _characterStatHolder.GetStat(out _healthStat);
            _healthStat.ValueChanged += UpdateHealthText;
            UpdateHealthText(_healthStat.Value);
            
            _characterStatHolder.GetStat(out _damageStat);
            _damageStat.ValueChanged += UpdateDamageText;
            UpdateDamageText(_damageStat.Value);
        }

        private void OnDisable()
        {
            _healthStat.ValueChanged -= UpdateHealthText;
            _damageStat.ValueChanged -= UpdateDamageText;
        }

        private void UpdateDamageText(float newValue) => 
            _damageText.text = $"{newValue}";

        private void UpdateHealthText(float newValue) => 
            _healthText.text = $"{newValue}";
    }
}
