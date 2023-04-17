using MainGame.CharacterResources.Interfaces;
using MainGame.UI.FloatValue;
using UnityEngine;

namespace MainGame.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private FloatValueView _healthView;
        private IHealth _health;
        
        private void Awake()
        {
            _health = GetComponentInParent<IHealth>();
        }

        private void OnDestroy()
        {
            if(_health != null) 
                _health.ValueChanged -= UpdateHealthView;
        }
        
        public void Construct(IHealth health)
        {
            _health ??= health;
            _health.ValueChanged += UpdateHealthView;
            UpdateHealthView(_health.Current, _health.Current);
        }

        private void UpdateHealthView(float oldValue, float newValue) => 
            _healthView.SetValue(newValue, _health.Max);
    }
}