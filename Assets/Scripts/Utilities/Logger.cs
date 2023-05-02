using UnityEngine;

namespace MainGame.Utilities
{
    public static class Logger
    {
        public static void Log(object message, object sender, Color color = default)
        {
#if UNITY_EDITOR
            string htmlColor = ColorUtility.ToHtmlStringRGB(color);
            Debug.Log($"<color=#{htmlColor}>{message}</color>. " +
                      $"Sender: <color=#{htmlColor}>{sender}</color>.");
#endif
        }
        
        public static void Log(object message, GameObject sender, Color color = default)
        {
#if UNITY_EDITOR
            string htmlColor = ColorUtility.ToHtmlStringRGB(color);
            Debug.Log($"<color=#{htmlColor}>{message}</color>. " +
                      $"Sender: <color=#{htmlColor}>{sender.name}</color>.");
#endif
        }
        
        public static void Log(object message, Color color = default)
        {
#if UNITY_EDITOR
            string htmlColor = ColorUtility.ToHtmlStringRGB(color);
            Debug.Log($"<color=#{htmlColor}>{message}</color>");
#endif
        }
    }
}