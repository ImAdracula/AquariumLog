using System;

namespace JessicasAquariumMonitor.Helpers.General
{
    public static class Guard
    {
        private const string DefaultParameterName = @"value";

        public static void ThrowIfNull(object value, string parameterName = DefaultParameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        public static void ThrowIfNullOrWhiteSpace(string value, string parameterName = DefaultParameterName)
        {
            ThrowIfNull(value, parameterName);

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(@"Parameter may not be whitespace", parameterName);
            }
        }
    }
}