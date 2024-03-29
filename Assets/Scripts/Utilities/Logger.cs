﻿using UnityEngine;

namespace MainGame.Utilities
{
    public static class Logger
    {
        public static void Log(string message, object sender, Color color = default)
        {
#if UNITY_EDITOR
            string htmlColor = ColorUtility.ToHtmlStringRGB(color);
            Debug.Log($"<color=#{htmlColor}>{message}</color>. " +
                      $"Sender: <color=#{htmlColor}>{sender}</color>.");
#endif
        }
        
        public static void Log(string message, GameObject sender, Color color = default)
        {
#if UNITY_EDITOR
            string htmlColor = ColorUtility.ToHtmlStringRGB(color);
            Debug.Log($"<color=#{htmlColor}>{message}</color>. " +
                      $"Sender: <color=#{htmlColor}>{sender.name}</color>.");
#endif
        }
        
        public static void Log(string message, Color color = default)
        {
#if UNITY_EDITOR
            string htmlColor = ColorUtility.ToHtmlStringRGB(color);
            Debug.Log($"<color=#{htmlColor}>{message}</color>");
#endif
        }
        
        public static void LogWarning(string message)
        {
#if UNITY_EDITOR
            string htmlColor = ColorUtility.ToHtmlStringRGB(Color.yellow);
            Debug.Log($"<color=#{htmlColor}>{message}</color>");
#endif
        }
        
        public static void LogError(string message)
        {
#if UNITY_EDITOR
            string htmlColor = ColorUtility.ToHtmlStringRGB(Color.red);
            Debug.Log($"<color=#{htmlColor}>{message}</color>");
#endif
        }
    }
}