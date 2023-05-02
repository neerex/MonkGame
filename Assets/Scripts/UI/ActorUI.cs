using DG.Tweening;
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
            if (_health != null)
            {
                _health.OnValueChanged -= UpdateHealthView;
                _health.OnValueChanged -= AnimateChange;
            } 
        }

        public void Construct(IHealth health)
        {
            _health ??= health;
            
            _health.OnValueChanged += UpdateHealthView;
            _health.OnValueChanged += AnimateChange;

            UpdateHealthView(_health.Current, _health.Current);
        }

        private void AnimateChange(float oldValue, float newValue)
        {
            DOVirtual.Vector3(Vector3.one,
                Vector3.one * 1.2f,
                0.1f,
                v => _healthView.transform.localScale = v
            ).SetEase(Ease.InOutQuint)
             .SetLoops(2, LoopType.Yoyo);
        }

        private void UpdateHealthView(float oldValue, float newValue) => 
            _healthView.SetValue(newValue, _health.Max);
    }
}