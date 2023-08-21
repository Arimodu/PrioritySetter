#if DEBUG
using System;
using System.Diagnostics;
using System.Threading;
using IPALogger = IPA.Logging.Logger;

namespace PrioritySetter
{
    internal class DebugChecker : IDisposable
    {
        readonly Timer Timer;
        readonly IPALogger Logger;
        readonly Plugin Instance;

        internal DebugChecker(int period, IPALogger logger, Plugin instance)
        {
            Timer = new Timer(CheckPriority, null, period, period);
            Logger = logger;
            Instance = instance;
        }

        private void CheckPriority(object state)
        {
            // Process object does not update, have to refresh manually
            var thisProcess = Process.GetCurrentProcess();

            Logger.Notice($"Cheking priority. Set: {thisProcess.PriorityClass}, Desired: {Instance.Priority}");

            if (thisProcess.PriorityClass != Instance.Priority)
            {
                Logger.Error($"Priority {thisProcess.PriorityClass} is not desired, refreshing manually!");
                Instance.SetPriority(null);
            }
        }

        public void Dispose()
        {
            Timer.Dispose();
        }
    }
}
#endif
