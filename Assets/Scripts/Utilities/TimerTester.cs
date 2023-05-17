using NaughtyAttributes;
using UnityEngine;

namespace MainGame.Utilities
{
    public class TimerTester : MonoBehaviour
    {
        [Button("Switch Time")]
        public void SwitchTime()
        {
            Time.timeScale = (Time.timeScale + 1) % 2;
        }
    }
}
