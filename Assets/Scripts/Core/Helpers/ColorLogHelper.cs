using UnityEngine;

namespace Queue.Helpers
{
    /// <summary>
    /// A class that have reference to colors that represents a system or object log message color
    /// </summary>
    public static class ColorLogHelper
    {
        public const string RED = "#ff0000";
        public const string GREEN = "#00ff00";
        public const string ENTITY_COLOR = "#ff00ff";
        public const string TARGETING_HANDLER_COLOR = "#cc4125";
        public const string ABILITY_HANDLER_COLOR = "#1155cc";
        public const string WAVE_MANAGER_COLOR = "#ffff00";
        public const string TIMER_HANDLER_COLOR = "#00ff00";
        public const string EFFECT_HANDLER_COLOR = "#5b0f00";
        public const string GAME_MANAGER_COLOR = "#ff0000";
        public const string CAMERA_HANDLER = "#00ffff";

        public static string SetColorToString(string message, Color color)
            => $"<color={ToRGBHex(color)}>{message}</color>";
        
        public static string SetColorToString(string message, string hexCode)
            => $"<color={hexCode}>{message}/<color>";
        
        public static string ToRGBHex(this Color c)
            => $"#{ToByte(c.r):X2}{ToByte(c.g):X2}{ToByte(c.b):X2}";
        
        private static byte ToByte(float f)
        {
            f = Mathf.Clamp01(f);
            return (byte)(f * 255);
        }
    }
}