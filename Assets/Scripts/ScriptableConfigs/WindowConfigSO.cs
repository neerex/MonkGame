using UnityEngine;

namespace MainGame.ScriptableConfigs
{
    [CreateAssetMenu(fileName = "WindowConfigs", menuName = "WindowsConfigs")]
    public class WindowConfigSO : ScriptableObject
    {
        public WindowID Id;
        public GameObject WindowPrefab;
    }

    public enum WindowID
    {
        MainMenu = 0,
        Options = 1,
        GameOver = 2
    }
}