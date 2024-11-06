using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.logging
{
    using System;
    using System.Diagnostics;

    public class MyLogger : IMyLogger
    {
        private static readonly Lazy<MyLogger> instance = new Lazy<MyLogger>(() => new MyLogger());

        private MyLogger()
        {
        }

        public static MyLogger Instance => instance.Value;

        public void RegisterListener(TraceListener listener)
        {
            if (listener != null && !Trace.Listeners.Contains(listener))
            {
                Trace.Listeners.Add(listener);
            }
        }

        public void UnregisterListener(TraceListener listener)
        {
            if (listener != null && Trace.Listeners.Contains(listener))
            {
                Trace.Listeners.Remove(listener);
            }
        }

        public void LogInfo(string message)
        {
            Trace.WriteLine($"INFO: {message}");
            Trace.Flush();
        }

        public void LogWarning(string message)
        {
            Trace.WriteLine($"WARNING: {message}");
            Trace.Flush();
        }

        public void LogError(string message)
        {
            Trace.WriteLine($"ERROR: {message}");
            Trace.Flush();
        }
    }

}
