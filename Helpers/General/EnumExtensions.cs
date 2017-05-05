using System;

namespace JessicasAquariumMonitor.Helpers.General
{
    public static class EnumExtensions
    {
        public static T Parse<T>(string value) where T : struct
        {
            return (T) Enum.Parse(typeof(T), value.Trim());
        }
    }
}