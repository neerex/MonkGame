using UnityEngine;
using UnityEngine.UI;

namespace MainGame.UI.FloatValue
{
    public class SliderView : FloatValueView
    {
        [SerializeField] private Slider _slider;
        
        public override void SetValue(float current, float max)
        {
            _slider.maxValue = max;
            _slider.value = current;
        }
    }
}