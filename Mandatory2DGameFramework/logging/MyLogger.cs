using System;
using System.Diagnostics;

namespace Mandatory2DGameFramework.logging
{
    public class MyLogger : IMyLogger
    {
        private static MyLogger? _instance;
        private readonly TraceSource _traceSource;

        private MyLogger()
        {
            _traceSource = new TraceSource("MyLogger");
        }

        public static MyLogger Instance => _instance ??= new MyLogger();

        public void RegisterListener(TraceListener listener)
        {
            _traceSource.Listeners.Add(listener);
        }

        public void UnregisterListener(TraceListener listener)
        {
            _traceSource.Listeners.Remove(listener);
        }

        public void LogInfo(string message)
        {
            _traceSource.TraceEvent(TraceEventType.Information, 0, message);
            Console.WriteLine($"INFO: {message}");
        }

        public void LogError(string message)
        {
            _traceSource.TraceEvent(TraceEventType.Error, 0, message);
            Console.WriteLine($"ERROR: {message}");
        }

        public void LogWarning(string message)
        {
            _traceSource.TraceEvent(TraceEventType.Warning, 0, message);
            Console.WriteLine($"WARNING: {message}");
        }

        public void LogDebug(string message)
        {
            _traceSource.TraceEvent(TraceEventType.Verbose, 0, message);
            Console.WriteLine($"DEBUG: {message}");
        }
    }
}


