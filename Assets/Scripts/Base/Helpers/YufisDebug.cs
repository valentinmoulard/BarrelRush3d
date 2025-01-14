using Base.GameManagement.Settings;
using UnityEngine;

namespace Base.Helpers
{
    public static class YufisDebug
    {
        public static void Log(string message, System.Drawing.Color drawingColor = default)
        {
            var unityColor = ConvertToUnityColor(drawingColor);
            Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(unityColor)}>{message}</color>");
        }
        
        public static void Log(int message, System.Drawing.Color drawingColor)
        {
            var unityColor = ConvertToUnityColor(drawingColor);
            Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(unityColor)}>{message.ToString()}</color>");
        }
        
        public static void Log(float message, System.Drawing.Color drawingColor)
        {
            var unityColor = ConvertToUnityColor(drawingColor);
            Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(unityColor)}>{message.ToString()}</color>");
        }

        private static Color ConvertToUnityColor(System.Drawing.Color drawingColor)
        {
            return new Color(drawingColor.R / 255f, drawingColor.G / 255f, drawingColor.B / 255f, drawingColor.A / 255f);
        }

        public static void Error(string message)
        {
            if (!Settings_General.Instance.DebugActivity) return;
            Debug.LogError("<color=red>" + message + "</color>");
        }
    }
}