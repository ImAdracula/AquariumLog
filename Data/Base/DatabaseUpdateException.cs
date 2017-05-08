using System;

namespace JessicasAquariumMonitor.Data.Base
{
    public sealed class DatabaseUpdateException : Exception
    {
        public DatabaseUpdateException(string message, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}