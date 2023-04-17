using UnityEngine;

namespace MainGame.UI.FloatValue
{
    public abstract class FloatValueView : MonoBehaviour
    {
        public abstract void SetValue(float current, float max);
    }
}