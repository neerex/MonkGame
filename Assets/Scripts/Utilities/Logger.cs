using UnityEngine;

namespace MainGame.Utilities
{
    public static class Logger
    {
        public static void Log(object message, object sender, Color color = default)
        {
            string htmlColor = ColorUtility.ToHtmlStringRGB(color);
            Debug.Log($"<color=#{htmlColor}>{message}</color>. " +
                      $"Sender: <color=#{htmlColor}>{sender}</color>.");
        }
        
        public static void Log(object message, GameObject sender, Color color = default)
        {
            string htmlColor = ColorUtility.ToHtmlStringRGB(color);
            Debug.Log($"<color=#{htmlColor}>{message}</color>. " +
                      $"Sender: <color=#{htmlColor}>{sender.name}</color>.");
        }
        
        public static void Log(object message, Color color = default)
        {
            string htmlColor = ColorUtility.ToHtmlStringRGB(color);
            Debug.Log($"<color=#{htmlColor}>{message}</color>");
        }
    }
}