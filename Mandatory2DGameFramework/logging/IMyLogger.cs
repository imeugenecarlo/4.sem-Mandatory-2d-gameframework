using System.Diagnostics;

namespace Mandatory2DGameFramework.logging
{
    public interface IMyLogger
    {
        void RegisterListener(TraceListener listener);
        void UnregisterListener(TraceListener listener);
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message);
    }
}