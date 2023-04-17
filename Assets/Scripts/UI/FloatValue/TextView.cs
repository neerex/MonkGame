using TMPro;
using UnityEngine;

namespace MainGame.UI.FloatValue
{
    public class TextView : FloatValueView
    {
        [SerializeField] private TextMeshProUGUI _tmp;
        
        public override void SetValue(float current, float max)
        {
            _tmp.text = $"{(int)current}/{(int)max}";
        }
    }
}